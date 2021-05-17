# ChangeLog

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
  
### BugFix

* 修复 TapUserRelationShip 中 mutualAttention 相互

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