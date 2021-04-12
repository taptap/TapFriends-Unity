# TapTap.Friends

## 使用前提

使用TapFriends的前提是必须添加以下依赖库:
* [TapTap.Bootstrap](https://github.com/TapTap/TapBootstrap-Unity.git)
* [TapTap.Common](https://github.com/TapTap/TapCommon-Unity.git)

## 接口描述

### 命名空间
```c#
using TapTap.Friends;
```

### 添加好友

```c#
TapFriends.AddFriend(string userId, Action<TapError> action);
```

### 删除好友
```c#
TapFriends.DeleteFriend(string userId, Action<TapError> action);
```

### 获取关注列表
```c#
TapFriends.GetFollowingList(int from, bool mutualAttention, int limit, Action<List<TapUserRelationShip>, TapError> action);
```

### 获取粉丝列表
```c#
TapFrineds.GetFollowerList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
```

### 获取黑名单列表
```c#
TapFriends.GetBlockList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
```

### 拉黑
```c#
TapFriends.GetBlockList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
```

### 解除黑名单
```c#
TapFriends.UnblockUser(string userId, Action<TapError> action);
```

