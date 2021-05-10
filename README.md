# 使用 TapSDK

## 前提条件

    TapSDK 仅支持 Android、iOS 平台。

* Unity 2018.3+
* .NET Core 2.0+
* XCode 12.+
* 确保你的 Unity 项目满足以下要求：
    * **对于iOS** - 目标为 iOS 10 或更高版本
    * **对于Android** - 目标为 API 级别 21 或更高版本

## **第一步:** 添加 TapSDK 配置文件

1. 创建 TDS-Info.plist [配置文件](./CONFIG.md)。


2. 打开 Unity 项目的 Project 窗口，然后在配置文件移到 **Assets/Plugins/iOS/Resource** 文件夹中。

## **第二步:** 添加 TapSDK

添加 TapSDK 有以下几种方式

1. 点击下载 **TapSDK.zip**，然后将该 SDK 解压到方便的位置。

    * 在你打开的 Unity 项目中，依次转到 **Assets > Import Packages > Custom Packages**。
    * 从解压缩中的 **TapSDK** 中，选择您希望在应用中使用的 **TapSDK** 产品导入。
        * TapTap_Bootstrap.unitypackage ： **必选**。TapSDK 启动器
        * TapTap_Common.unitypackage ： **必选**。TapSDK 基础库
        * TapTap_Login.unitypackage ：**必选** TapTap 登陆
        * TapTap_Moment.unitypackage ： TapTap 内嵌动态
        * TapTap_TapDB.unitypackage ：TapDB 数据统计
        * TapTap_Friends.unitypackage: TapFriends 好友系统
        * TapTap_License.unitypackage: TapLicense DLC 购买认证


2.  使用 Unity Package Manager 导入 TapSDK。

    * 在你打开的 Unity 项目中，依次转到 **Packages/manifest.json**，并添加以下代码。
    ```json
    {
        "dependencies":{
          "com.taptap.tds.bootstrap": "https://github.com/TapTap/TapBootstrap-Unity.git#{tag}",
          "com.taptap.tds.common": "https://github.com/TapTap/TapCommon-Unity.git#{tag}",
          "com.taptap.tds.login": "https://github.com/TapTap/TapLogin-Unity.git#{tag}",
          "com.taptap.tds.tapdb": "https://github.com/TapTap/TapDB-Unity.git#{tag}",
          "com.taptap.tds.moment": "https://github.com/TapTap/TapMoment-Unity.git#{tag}",
          "com.taptap.tds.license": "https://github.com/TapTap/TapLicense-Unity.git#{tag}",
          "com.taptap.tds.friends": "https://github.com/TapTap/TapFriends-Unity.git#{tag}"
        }
    }
    ```
## 文档
  
### [API文档](https://taptap.github.io/TapSDK-Unity/html/)