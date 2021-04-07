#!/bin/sh

unityPath=/Applications/Unity/Hub/Editor/2018.4.30f1/Unity.app/Contents/MacOS/Unity

path=$(cd "$(dirname "$0")";pwd)

echo $path

$unityPath -projectPath $path -batchmode -executeMethod Editor.ExportPackage.PushUnityPackage -quit