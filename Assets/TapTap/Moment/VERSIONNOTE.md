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
  
### Dependencies

- TapTap.Common