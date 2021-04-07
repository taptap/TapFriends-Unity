#!/bin/sh

UNITY_PATH=/Applications/Unity/Hub/Editor/$1/Unity.app/Contents/MacOS/Unity
# shellcheck disable=SC2164
PROJECT_PATH=$(cd "$(dirname "$0")";pwd)

$UNITY_PATH -buildTarget iOS -batchmode -projectPath "$PROJECT_PATH" -executeMethod Editor.ExportPackage.PackageIOS -quit

Project_Folder="TapSDK2-Unity"
#工程名字(Target名字)
Project_Name="Unity-iPhone"
#配置环境，Release或者Debug
Configuration="Release"
#加载各个版本的plist文件
EnterpriseExportOptionsPlist=$PROJECT_PATH/ExportOptions.plist

ExportIPAPath=$2

xcodebuild -project $PROJECT_PATH/$Project_Folder/$Project_Name.xcodeproj -scheme $Project_Name -configuration $Configuration -archivePath $PROJECT_PATH/$Project_Folder/build/$Project_Name-enterprise.xcarchive clean archive build

xcodebuild -exportArchive -archivePath $PROJECT_PATH/$Project_Folder/build/$Project_Name-enterprise.xcarchive -exportOptionsPlist $EnterpriseExportOptionsPlist -exportPath $ExportIPAPath
