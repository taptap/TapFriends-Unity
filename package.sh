#!/bin/sh

unityVersion=2018.4.30f1

PROJECT_PATH=$(cd "$(dirname "$0")";pwd)


sh ./iOS_package.sh $unityVersion $1

sh ./android_package.sh $unityVersion $1