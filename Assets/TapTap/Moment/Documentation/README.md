## TapTap.Moment

## 1.命名空间

```c#
using TapTap.Moment;
```


## 2.接口描述

### 设置回调

```c#
TapMoment.SetCallback(Action<int,string> action);
```

### 打开动态

```c#
TapMoment.Open(Orientation orientation);
```

### 发布动态

```c#
TapMoment.Publish(Orientation orientation, string[] imagePaths, string content);
```

### 关闭动态

```c#
TapMoment.Close();
```

### 获取更新消息
```c#
TapMoment.FetchNotification();
```
