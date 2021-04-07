#!/bin/sh

UNITY_PATH=/Applications/Unity/Hub/Editor/$1/Unity.app/Contents/MacOS/Unity
# shellcheck disable=SC2164
PROJECT_PATH=$(cd "$(dirname "$0")";pwd)

echo "Start To Build Android APK in $PROJECT_PATH"

$UNITY_PATH -buildTarget Android -batchmode -projectPath $PROJECT_PATH -executeMethod Editor.ExportPackage.PackageAndroid -UNITY_VERSION=$1 -EXPORT_PATH=$2 -quit
