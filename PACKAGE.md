# TapSDK2-Unity

## 前提条件

    TapSDK 仅支持 Android、iOS 平台。

* Unity 2018.3+
* .NET Core 2.0+
* XCode 12.+
* 确保你的 Unity 项目满足以下要求：
    * **对于iOS** - 目标为 iOS 10 或更高版本
    * **对于Android** - 目标为 API 级别 21 或更高版本

## 文档简介

为了方便开发自行打包 Unity Demo 自测以及保证提测质量，以下介绍 TapSDK2-Unity 工程以及 Android、iOS 开发同学打包流程。

## 目录简介

```
Project
 |-- PACKAGE.md // 编译流程文件
 |-- REAMD.md // 接入文档
 |-- Assets // Unity 项目工程
 |       |-- TapTap // TapSDK 业务模块
 |             ｜-- Bootstrap // TapSDK 启动器
 |                    |-- Plugins // Native 库
 |                           |--- Android
 |                                ｜-- libs // Android Native 库目录
 |                           |--- iOS
 |                                |-- Resource // iOS 资源文件目录
 |                                |-- TapBootstrapSDK.framework // iOS Native framework
 |             | -- Common // TapSDK Common 库    
 |             | -- Moment // TapSDK 内嵌动态
 |             | -- TapDB // TapDB 数据统计
 |             | -- Friends // 好友
 |             | -- Login // TapTap 登陆模块
 |       ...
 |...                 
```

## 打包工作流

> 以下以替换 **TapBootstrap** 模块举例，以 Feat/bootstrap_feat 分支为例

1. 拉取 Feat/bootstrap_feat 分支
2. 替换 Android 以及 iOS native 库
    * 使用最新的 TapBoostrap_new.aar 目录 Assets/TapTap/Boostrap/Plugins/Android/libs 目录中的aar 
    * 使用最新的 TapBootstrapSDK.framework 以及 TapBootstrapResource.bundle 替换 Assets/TapTap/Bootstrap/Plugins/iOS 中的 framework 以及 Bundle
3. 提交 Merge 到 Master 分支
4. 自动触发 CI 打包 并输出到 Slack 中 #tds-client-sdk-ci channel 中。








