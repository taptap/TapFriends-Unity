# 使用 TapSDK

## 前提条件

* 安装 2018.3 或更高版本。
* （仅适用于iOS）安装以下应用
    * XCode 12.+ 或者更高版本
* 确保你的 Unity 项目满足以下要求：
    * **对于iOS** - 目标为 iOS 10 或更高版本
    * **对于Android** - 目标为 API 级别 21 或更高版本

## **第一步:** 添加 TapSDK 配置文件

1. 创建 TDS-Info.plist 配置文件。
    * [配置文件详情](./CONFIG.md)
2. 打开 Unity 项目的 Project 窗口，然后在配置文件移到 **Assets/Plugins/iOS/Resource** 文件夹中。

## **第二步:** 添加 TapSDK

添加 TapSDK 有以下几种方式

    TapSDK 仅支持 Android、iOS 平台。

1. 点击下载 **TapSDK.zip**，然后将该 SDK 解压到方便的位置。

    * 在你打开的 Unity 项目中，依次转到 **Assets > Import Packages > Custom Packages**。
    * 从解压缩中的 **TapSDK** 中，选择您希望在应用中使用的**TapSDK**产品导入。
        * TapBootstrapSDK.unitypackage ： **必选**。TapSDK 启动器
        * TapCommonSDK.unitypackage ： **必选**。TapSDK 基础库
        * TapLoginSDK.unitypackage ：**必选** TapTap登陆
        * TapMomentSDK.unitypackage ： TapTap内嵌动态
        * TapDBSDK.unitypackage ：TapDB 数据统计


2.  使用 Unity Package Manager 导入 TapSDK。

    * 在你打开的 Unity 项目中，依次转到 **Packages/manifest.json**，并添加以下代码。
    ```json
    {
        "dependencies":{
          "com.tapsdk.bootstrap": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}",
          "com.tapsdk.common": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}",
          "com.tapsdk.login": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}",
          "com.tapsdk.tapdb": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}",
          "com.tapsdk.moment": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}"
        }
    }
    ```