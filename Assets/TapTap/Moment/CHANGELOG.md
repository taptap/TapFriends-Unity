# ChangeLog

## 2.1.6

### Optimization and fixed bugs

- 修复调用 [TapMoment close] 不生效的 bug

### Dependencies

- TapTap.Common v2.1.6

## 2.1.5

### New Feature

- 新增场景化回调接口

  ```
  //场景化回调是在动态回调的统一接口中返回，Code = 70000，内容为 JSON 格式的字符串
  {
    sceneId: "taprl0071417002",
    eventType: "READY",
    eventPayload: "{}",
    timestamp: 1622791814130, // ms
  }
  ```

## 2.1.4

### Dependencies

- TapTap.Common v2.1.4

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
