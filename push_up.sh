#!/bin/sh

unityPath=/Applications/Unity/Hub/Editor/2019.4.1f1/Unity.app/Contents/MacOS/Unity

path=$(cd "$(dirname "$0")";pwd)

echo $path

$unityPath -projectPath $path  -executeMethod Editor.ExportPackage.PushUnityPackage -VERSION=$1 
