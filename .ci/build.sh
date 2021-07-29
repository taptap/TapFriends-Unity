#!/bin/sh
export LANG="en_US.UTF-8"

BUILD_TYPE=$1

buildFail() {
    java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build " --body="<${CI_JOB_URL}|Package Failed>"
    exit 1
}

java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} build " --body="<${CI_JOB_URL}|Package Start>"

PRODUCT_DIR=./Products

# 生成Product目录
mkdir -p $PRODUCT_DIR

#编译 Module 
#sh ./compile.sh

sh ./package.sh $PRODUCT_DIR

zip -q -ry ${CI_PROJECT_TITLE}-tapsdk-macosx.zip $PRODUCT_DIR/TapSDK2-Unity.app

java -jar .ci/release.jar nb --af=${CI_PROJECT_TITLE}-tapsdk-macosx.zip

sh ./pkg_uploader.sh $PRODUCT_DIR/TapSDK2-Unity.apk com.tds.demo

sh ./pkg_uploader.sh $PRODUCT_DIR/demo.ipa com.tdssdk.demo

