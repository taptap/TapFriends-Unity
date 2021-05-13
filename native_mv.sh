#!/bin/sh

## Project 目录新建 NativePackages 文件夹，Copy iOS Android Binary 进去，执行该脚本替换。

rootPath=$(cd `dirname $0`; pwd) 

version=2.1.1

module=("Bootstrap" "Common" "Moment" "FriendsUI" "Friends" "License" "Login"  "TapDB")

releaseAndroid=false

releaseIOS=true

function RemoveNativePackage(){
  if [ $releaseAndroid == true ];then
      rm -fr $rootPath/Assets/TapTap/$1/Plugins/Android/libs/*
  fi
  
  if [ $releaseIOS == true ];then
      rm -fr $rootPath/Assets/TapTap/$1/Plugins/iOS/*
  fi
}

function CopyNativePackage(){
  cp -R $1 $2
}

for ((i=0;i<${#module[@]};i++));do
  
    RemoveNativePackage ${module[$i]} 
  
    echo "Start to Copy ${module[$i]} binary"   
    
    aar=Tap${module[$i]}_$version.aar
    bundle=Tap${module[$i]}Resource.bundle
    framework=Tap${module[$i]}SDK.framework
    
    
    
    if [ "${module[$i]}" == "TapDB" ];then
        aar=TapDB_$version.aar
        framework=TapDB.framework
    fi  

    if [ "${module[$i]}" == "Friends" ];then
        aar=TapFriend_$version.aar
        framework=TapFriendSDK.framework
    fi
  
    if [ "${module[$i]}" == "FriendsUI" ]; then
        aar=TapFriendUI_$version.aar
        bundle=TapFriendResource.bundle
        framework=TapFriendUISDK.framework
    fi
  
    echo "Android Binary $aar"   
    if [ $releaseAndroid == true ];then
        CopyNativePackage $rootPath/NativePackages/$aar $rootPath/Assets/TapTap/${module[$i]}/Plugins/Android/libs/$aar
    fi
    
    if [ $releaseIOS == true ];then
      if [ "${module[$i]}" == "Bootstrap" ] || [ "${module[$i]}" == "Common" ] || [ "${module[$i]}" == "Moment" ] || [ "${module[$i]}" == "FriendsUI" ];then
          echo "iOS Bundle $bundle"   
          mkdir -p $rootPath/Assets/TapTap/${module[$i]}/Plugins/iOS/Resource 
          CopyNativePackage $rootPath/NativePackages/$bundle  $rootPath/Assets/TapTap/${module[$i]}/Plugins/iOS/Resource/$bundle
      fi
    
      if [ "${module[$i]}" != "License" ];then
          echo "iOS framework $framework"  
          CopyNativePackage $rootPath/NativePackages/$framework $rootPath/Assets/TapTap/${module[$i]}/Plugins/iOS/$framework
      fi
    fi
done   
