//
//  TapTapForum.h
//  TapTapSDK
//
//  Created by JiangJiahao on 2019/11/21.
//  Copyright © 2019 taptap. All rights reserved.
//

#import <TapMomentSDK/TapMomentDelegate.h>
#import <TapMomentSDK/TapMomentConfig.h>
#import <TapMomentSDK/TapMomentPostData.h>
#import <TapMomentSDK/TapMomentResultCode.h>
#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 v1.2.0
 */

# pragma mark SDK 公开方法
@interface TapMoment : NSObject

+ (instancetype)new NS_UNAVAILABLE;

- (instancetype)init NS_UNAVAILABLE;

/// 初始化
/// @param clientId clienId
/// 默认国内
+ (void)initWithClientId:(NSString *)clientId;

/// 初始化
/// @param clientId clientId
/// @param isCN 是否国内
+ (void)initWithClientId:(NSString *)clientId isCN:(BOOL)isCN;

/// 设置代理
/// @param delegate 代理
+ (void)setDelegate:(id <TapMomentDelegate>)delegate;

/// 打开动态
/// @param config TapMomentConfig
+ (void)open:(TapMomentConfig *)config;

/// 打开好友个人中心
/// @param config TapMomentConfig
/// @param userId openId
+ (void)openUserCenter:(TapMomentConfig *)config
                userId:(NSString *)userId;

/// 打开动态场景化入口
/// @param config TapMomentConfig
/// @param sceneId 场景化入口id
+ (void)openSceneEntry:(TapMomentConfig *)config
               sceneId:(NSString *)sceneId;

/// 发布动态
/// @param content 发布的动态内容
/// @param config 配置
/// @warning `moment`参数：动态内容为视频请选择`TapMomentVideoData`, 动态内容为图片时请选择 `TapMomentImageData`。
+ (void)publish:(TapMomentConfig *_Nonnull)config
        content:(TapMomentPostData *_Nonnull)content;

/// 关闭所有内嵌窗口
/// @param title 弹窗标题
/// @param content 弹窗内容
/// @param showConfirm 是否显示确认弹窗
/// @warning 传空或者空串不弹框
+ (void)closeWithTitle:(NSString *)title
               content:(NSString *)content
           showConfirm:(BOOL)showConfirm;

/// 直接关闭所有内嵌窗口
/// @warning 该方法不弹二次确认窗口
+ (void)close;

/// 获取新动态数量
/// @warning 结果在 `Delegate` 下的 `onMomentCallbackWithCode:msg:`, code == TM_RESULT_CODE_NEW_MSG_SUCCEED时，`msg` 即为消息数量
+ (void)fetchNotification;

/// 获取SDK版本名
+ (NSString *)getSdkVersion;

/// 获取SDK版本号
+ (NSString *)getSdkVersionCode;
@end

NS_ASSUME_NONNULL_END
