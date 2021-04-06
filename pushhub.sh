#!/bin/sh
# 获取当前分支
currentBranch=$(git symbolic-ref --short -q HEAD)

git branch -D main

# shellcheck disable=SC2039
var=("tapcommon-upm" "tapdb-upm" "tapmoment-upm" "tapbootstrap-upm" "taplogin-upm")
# shellcheck disable=SC2039
module=("TapCommonSDK" "TapDBSDK" "TapMomentSDK" "TapBootstrapSDK" "TapLoginSDK")

tag=$1

function pushGithub(){  
  git tag -d $(git tag)
  git subtree split --prefix=Assets/$1 --branch main
  git checkout $2 --force
  git tag $3
  git push $2 main --force --tags
  git checkout $currentBranch --force
}

for ((i=0;i<${#var[@]};i++));do
    pushGithub ${module[$i]} ${var[$i]} $tag
done   
