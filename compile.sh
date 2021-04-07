#!/bin/sh

module=("TapCommonSDK" "TapDBSDK" "TapMomentSDK" "TapBootstrapSDK" "TapFriendsSDK")

rootPath=$(cd `dirname $0`; pwd) 

echo $rootPath

function compileDll(){
    cd TapSDK/$1
    dotnet build -c Release
    
    CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/$1.dll $rootPath/Assets/$1/Plugins/$1.dll
    CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/$1.pdb $rootPath/Assets/$1/Plugins/$1.pdb
    
    echo $(cd `dirname $0`; pwd) 
    cd ../..
    echo $(cd `dirname $0`; pwd) 

    if [ "$1" == "TapCommonSDK" ];then
        dotnet build -c IOS
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/$1.dll $rootPath/Assets/$1/Plugins/iOS/$1.dll
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/$1.pdb $rootPath/Assets/$1/Plugins/iOS/$1.pdb
        echo "Copy TapCommonSDK to $rootPath/Assets/$1/Plugins/iOS"
    fi
}

function CopyAndReplease(){
   cp -r $1 $2
}

for ((i=0;i<${#module[@]};i++));do
    compileDll ${module[$i]}
done   