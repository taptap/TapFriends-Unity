#!/bin/sh
# 获取当前分支
currentBranch=$(git symbolic-ref --short -q HEAD)

git branch -D tapcommon-upm

git branch -D tapbootstrap-upm 

git branch -D tapmoment-upm

git branch -D taplogin-upm 

git branch -D tapdb-upm

# 删除gitlab的tag以及本地缓存的Tag
# shellcheck disable=SC2046
git tag -d $(git tag)

git config --local http.postBuffer 524288000

#var=("tapcommon-upm" "tapdb-upm" "tapmoment-upm" "tapbootstrap-upm" "taplogin-upm")
var=("tapcommon-upm")

function pushhub(){
  git subtree split --prefix=Assets/$1 --branch $1
  git checkout $1 --force
  git tag $2
  git push $1 $1 --force --tags
  git checkout $currentBranch --force
}

# shellcheck disable=SC2068
for str in ${var[@]}; do

echo $str

pushhub $str $1

done

echo $currentBranch