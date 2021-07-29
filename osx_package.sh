#!/bin/sh

UNITY_PATH=/Applications/Unity/Hub/Editor/$1/Unity.app/Contents/MacOS/Unity
# shellcheck disable=SC2164
PROJECT_PATH=$(cd "$(dirname "$0")";pwd)

echo "Start To Build MacOSX App in $PROJECT_PATH"

$UNITY_PATH -buildTarget StandaloneOSX -batchmode -quit -projectPath $PROJECT_PATH -executeMethod Editor.ExportPackage.PackageMacOSX -UNITY_VERSION=$1 -EXPORT_PATH=$2 
