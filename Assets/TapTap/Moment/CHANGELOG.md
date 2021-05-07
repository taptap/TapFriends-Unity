# ChangeLog

## 2.1.0

### Feature

* 新增 DirectlyOpen 接口
    * 场景化入口
  ```c#
  var sceneDic = new Dictionary<string,object>(){{TapMomentPageShortCutKey,sceneId}};

  TapMoment.DirectlyOpen(orientation,TapMomentConstants.TapMomentPageShortCut,sceneDic);
  ```
    * 用户中心入口
  ```c#
  var userDic = new Dictionary<string,object>(){{TapMomentPageUserKey,userId}};
  
  TapMoment.DirectlyOpen(orientation,TapMomentConstants.TapMomentPageUser,userDic);
  ```

### Dependencies

* TapTap.Bootstrap v2.1.0
* TapTap.Common v2.1.0

## 2.0.0

### Dependencies

* TapTap.Bootstrap v2.0.0
* TapTap.Common v2.0.0
