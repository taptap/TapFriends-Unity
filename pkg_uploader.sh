#!/bin/sh


IPA_PATH=$1

BUNDLE_ID=$2
MASK_ID=""

isiOS() {
  if [ "${IPA_PATH##*.}"x = "ipa"x ];then
      return 0
  else
      return 1
  fi
}
 

app_budnle_id() {
  if isiOS; then
    output="./.temp_plist"
    mkdir -p $output
    unzip -j -o -q "${IPA_PATH}" -d "${output}" '*.app/Info.plist'
    plist="${output}/Info.plist"
    BUNDLE_ID=$(/usr/libexec/PlistBuddy -c "Print CFBundleIdentifier" "${plist}")
    rm -rf $output
  else 
    # 需要设置aapt2的环境变量
    BUNDLE_ID=$(echo $(aapt2 dump $IPA_PATH) | sed -e 's/.*name=\(.*\)id=.*type anim.*/\1/')
  fi
  
}

if [ "" = "$BUNDLE_ID" ];then
  app_budnle_id
fi


resp=$(curl https://pkg.xindong.com/upload.php \
    -X POST \
    -F "file=@${IPA_PATH};type=application/octet-stream" \
    -F 'memo=')

get_pkg_id_script="
import sys, json, re
url = json.load(sys.stdin)['data']['url']
id = re.findall('\?id=(\w+)$', url)
print(id[0])
"
ipa_id=$(echo "${resp}" | python -c "${get_pkg_id_script}")

get_maskid_script="
import sys, json
list = json.load(sys.stdin)['rows']
maskid = [item for item in list if item['id'] == ${ipa_id}][0]['maskid']
print(maskid)
"

pkg_mask_id() {
  APK_LIST_URL="https://pkg.xindong.com/pages/pkg/opts/read.php?q=${BUNDLE_ID}&limit=20&"
  ipa_list_result=$(curl "${APK_LIST_URL}")
  MASK_ID=$(echo "${ipa_list_result}" | python -c "${get_maskid_script}")
}
pkg_mask_id
pkg_mask_id

echo $MASK_ID

Android_Url=""

iOS_Url=""

if isiOS ; then
  iOS_Url="https://pkg.xindong.com/i.php?id=${MASK_ID}&package=${BUNDLE_ID}"
else 
  Android_Url="https://pkg.xindong.com/i.php?id=${MASK_ID}&package=${BUNDLE_ID}&apk=1"
fi

java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build Success" --body="<${iOS_Url}|iOS 下载地址>"
java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build Success" --body="<${Android_Url}|Android 下载地址>"
