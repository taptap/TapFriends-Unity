#!/bin/sh

core=("Common" "TapDB" "Moment" "Bootstrap" "Friends")

module=("TapCommonSDK" "TapDBSDK" "TapMomentSDK" "TapBootstrapSDK" "TapFriendsSDK")

rootPath=$(cd `dirname $0`; pwd) 

echo $rootPath

function compileDll(){
    cd TapSDK/$1
    dotnet restore $1.sln
    dotnet build -c Release
    
    CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.dll
    CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.pdb
    
    if  [ "$1" == "TapCommonSDK" ];then
        dotnet restore $1.sln
        dotnet build -c IOS
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.dll
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.pdb
        echo "Copy $1 to $rootPath/Assets/TapTap/$1/Plugins/iOS"
    fi
    
    if  [ "$1" == "TapDBSDK" ];then
        dotnet restore $1.sln
        dotnet build -c IOS
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.dll
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.pdb
        echo "Copy $1 to $rootPath/Assets/TapTap/$1/Plugins/iOS"
    fi
    
    echo $(cd `dirname $0`; pwd) 
    cd ../..
    echo $(cd `dirname $0`; pwd) 
}

function CopyAndReplease(){
   cp -r $1 $2
}

for ((i=0;i<${#module[@]};i++));do
    compileDll ${module[$i]} ${core[$i]} 
done   