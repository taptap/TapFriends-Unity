#!/bin/sh

#core=("Common" "TapDB" "Moment" "Bootstrap" "Friends" "Login" "License" "Achievement")
core=("Common" "TapDB" "Moment" "Bootstrap" "Login" "License" "Achievement" "Support")

module=("TapCommonSDK" "TapDBSDK" "TapMomentSDK" "TapBootstrapSDK" "TapLoginSDK" "TapLicense" "TapAchievementSDK" "TapSupport")
#module=("TapCommonSDK" "TapDBSDK" "TapMomentSDK" "TapBootstrapSDK" "TapFriendsSDK" "TapLoginSDK" "TapLicense" "TapAchievementSDK")

rootPath=$(cd `dirname $0`; pwd) 

echo $rootPath

function compileDll(){
  
    cd TapSDK/$1

    if  [ "$1" == "TapBootstrapSDK" -o  "$1" == "TapSupport" ];then
        cd $1
        dotnet build -c Release
        CopyAndReplease $rootPath/TapSDK/$1/$1/bin/Release/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.dll
        CopyAndReplease $rootPath/TapSDK/$1/$1/bin/Release/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.pdb
        echo "Copy TapBootstrapSDK to $rootPath/Assets/TapTap/Bootstrap/Plugins/iOS"
        cd ..
    else 
        dotnet build -c Release
        CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.dll
        CopyAndReplease $rootPath/TapSDK/$1/bin/Release/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/TapTap.$2.pdb
    fi 
    
    if  [ "$1" == "TapCommonSDK" ];then
        dotnet build -c IOS
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.dll $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.dll
        CopyAndReplease $rootPath/TapSDK/$1/bin/iOS/netstandard2.0/TapTap.$2.pdb $rootPath/Assets/TapTap/$2/Plugins/iOS/TapTap.$2.pdb
        echo "Copy $1 to $rootPath/Assets/TapTap/$1/Plugins/iOS"
    fi
    
    if  [ "$1" == "TapDBSDK" ];then
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
    
    echo "Copy path $1 to $2"
  
   cp -r $1 $2
}

for ((i=0;i<${#module[@]};i++));do
    compileDll ${module[$i]} ${core[$i]} 
done   