#!/bin/sh

unityPath=/Applications/Unity/Hub/Editor/2019.4.28f1/Unity.app/Contents/MacOS/Unity

path=$(cd "$(dirname "$0")";pwd)

echo $path

$unityPath -projectPath -batchmode $path  -executeMethod Editor.ExportPackage.PushUnityPackage -VERSION=$1 -quit
