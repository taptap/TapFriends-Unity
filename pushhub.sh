#!/bin/sh
# 获取当前分支
currentBranch=$(git symbolic-ref --short -q HEAD)

git branch -D tapcommon-upm

git branch -D tapbootstrap-upm 

git branch -D tapmoment-upm

git branch -D taplogin-upm 

git branch -D tapdb-upm

git config --local http.postBuffer 524288000

#var=("tapcommon-upm" "tapdb-upm" "tapmoment-upm" "tapbootstrap-upm" "taplogin-upm")
var=("tapcommon-upm")
module=("TapCommon")
#module=("TapCommon" "TapDB" "TapMoment" "TapBootstrap" "TapLogin")

tag=$1

function pushhub(){  
  git tag -d $(git tag)
  git subtree split --prefix=Assets/$1 --branch $2
  git checkout $2 --force
  git tag $3
  git push $2 $2 --force --tags
  git checkout $currentBranch --force
}

for ((i=0;i<${#var[@]};i++));do
    pushhub ${module[$i]} ${var[$i]} $tag
done   

echo $currentBranch