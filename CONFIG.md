# TapSDK 配置文件

配置文件会和 TapSDK 项目以及资源相关联。

## TDS-Info.plist 示例

```xml
<?xml version="1.0" encoding="UTF-8"?>
<plist version="1.0">
  <dict>
    <key>taptap</key>
    <dict>
      <key>client_id</key>
      <string>0RiAlMny7jiz086FaU</string>
    </dict>
    <key>NSPhotoLibraryUsageDescription</key>
    <string>App需要您的同意,才能访问相册</string>
    <key>NSCameraUsageDescription</key>
    <string>App需要您的同意,才能访问相机</string>
    <key>NSMicrophoneUsageDescription</key>
    <string>App需要您的同意,才能访问麦克风</string>
    <key>NSUserTrackingUsageDescription</key>
    <string>App需要追踪你的信息</string>
  </dict>
</plist>
```

* TapTap Client_Id: 项目在 TapTap 各个位置的唯一标识符
  
## 权限

* NSPhotoLibraryUsageDescription: TapMoment 所需权限 相册
* NSCameraUsageDescription: TapMoment 所需权限 相机
* NSMicrophoneUsageDescription: TapMoment 所需权限 麦克风
* NSUserTrackingUsageDescription: TapDB 所需权限 数据追踪


