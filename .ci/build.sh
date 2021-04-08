#!/bin/sh
export LANG="en_US.UTF-8"

BUILD_TYPE=$1

sNightly() {
    if [ "Nightly" = "$BUILD_TYPE" ];then
        echo "IS Nightly"
        return 0
    else
        echo "NOT Nightly"
        return 1
    fi
}

isRelease() {
    if [ "Release" = "$BUILD_TYPE" ];then
        echo "IS Release"
        return 0
    else
        echo "NOT Release"
        return 1
    fi
}

buildFail() {
    java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} $BUILD_TYPE build " --body="<${CI_JOB_URL}|Package Failed>"
    exit 1
}

java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} $BUILD_TYPE build " --body="<${CI_JOB_URL}|Package Start>"

PRODUCT_DIR=./Products

# 生成Product目录
mkdir -p $PRODUCT_DIR

#编译 Module 
sh ./compile.sh

sh ./package.sh $PRODUCT_DIR

ls -l $PRODUCT_DIR

zip -q -ry ${CI_PROJECT_TITLE}-$sdkVersion.zip $PRODUCT_DIR

echo "--------start upload file to test server---------"

java -jar .ci/release.jar nb --af=${CI_PROJECT_TITLE}-$sdkVersion.zip --ver=$sdkVersion

