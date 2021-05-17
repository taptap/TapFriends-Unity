#!/bin/sh
# 生成 Doc 文档
doxygen Doxyfile   

currentBranch=$(git symbolic-ref --short -q HEAD)

git branch -D gh-pages

git subtree split --prefix=Doc/ --branch gh-pages

git remote rm doc

git remote add doc git@github.com:TapTap/TapSDK-Unity.git

git checkout gh-pages --force

git push doc gh-pages --force

git checkout $currentBranch --force


