#!/bin/sh
export LANG="en_US.UTF-8"

BUILD_TYPE=$1

THREAD=""

buildFail() {
    java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build " --body="<${CI_JOB_URL}|Package Failed>"  --thread $THREAD
    exit 1
}

resultContent=$(java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build " --body="<${CI_JOB_URL}|Package Start>")
echo $resultContent
THREAD=${resultContent#*threadTs=}
echo $THREAD
PRODUCT_DIR=./Products

# 生成Product目录
mkdir -p $PRODUCT_DIR

#编译 Module 
#sh ./compile.sh

sh ./package.sh $PRODUCT_DIR

zip -q -ry ${CI_PROJECT_TITLE}-macosx.zip $PRODUCT_DIR/TapSDK2-Unity.app

java -jar .ci/release.jar nb --af=${CI_PROJECT_TITLE}-macosx.zip --thread $THREAD

sh ./pkg_uploader.sh $PRODUCT_DIR/demo.ipa com.tdssdk.demo $THREAD

sh ./pkg_uploader.sh $PRODUCT_DIR/TapSDK2-Unity.apk com.tds.demo $THREAD

cp -r $PRODUCT_DIR/demo.ipa /Users/gitlab-runner/Desktop/QA-Test/Ios/ipa/

cp -r $PRODUCT_DIR/TapSDK2-Unity.apk /Users/gitlab-runner/Desktop/QA-Test/Android/apk/

