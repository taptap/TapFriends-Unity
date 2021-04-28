#!/bin/sh

unityVersion=2019.4.18f1c1

PROJECT_PATH=$(cd "$(dirname "$0")";pwd)


sh ./iOS_package.sh $unityVersion $1

sh ./android_package.sh $unityVersion $1