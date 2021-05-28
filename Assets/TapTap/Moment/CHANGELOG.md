# ChangeLog

## 2.1.3

### BugFixs

- Android 在部分刘海屏设备上判断失败导致UI可能被遮挡的问题

### Dependencies

- TapTap.Common v2.1.3

## 2.1.2

### Dependencies

- TapTap.Common v2.1.2

## 2.1.1

### Feature

* 新增 DirectlyOpen 接口
    * 场景化入口
  ```c#
  var sceneDic = new Dictionary<string,object>(){{TapMomentConstants.TapMomentPageShortCutKey,sceneId}};

  TapMoment.DirectlyOpen(orientation,TapMomentConstants.TapMomentPageShortCut,sceneDic);
  ```

### Dependencies

* TapTap.Bootstrap v2.1.1
* TapTap.Common v2.1.1

## 2.0.0

### Dependencies

* TapTap.Bootstrap v2.0.0
* TapTap.Common v2.0.0
