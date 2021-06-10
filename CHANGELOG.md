# ChangeLog

## 2.1.4

### New Feature

- 新增设置富信息、查询富信息接口
- TapUserRelationShip 新增 online（是否在线） & time（在线时长） & TapRichPresence（富信息）属性

### Dependencies

- TapTap.Common v2.1.4

## 2.1.3

### Dependencies

- TapTap.Common v2.1.3

## 2.1.2

### Feature
* 新增消息回调接口
  ```c#
  TapFriends.RegisterMessageListener(ITapMessageListener listener);
  ```  
* 搜索好友
    ```c#
    TapFriends.SearchUser(string userId, Action<TapUserRelationShip, TapError> action)
    ```  
* 分享好友邀请
  ```c#
  TapFriends.SendFriendInvitation(Action<bool, TapError> action);
  ```
* 获取好友邀请链接
  ```c#
  TapFriends.GenerateFriendInvitation(Action<string, TapError> action);
  ```

### Dependencies

* TapTap.Bootstrap v2.1.2
* TapTap.Common v2.1.2

## 2.1.1

### Dependencies

* TapTap.Bootstrap v2.1.1
* TapTap.Common v2.1.1

## 2.1.0

### Feature

* Friends

### Dependencies

* TapTap.Bootstrap v2.1.0
* TapTap.Common v2.1.0