//
//  TapFriends.h
//  TapFriends
//
//  Created by Bottle K on 2021/3/16.
//

#import <Foundation/Foundation.h>
#import <TapFriendSDK/TapUserRelationShip.h>
#import <TapCommonSDK/TapCommonSDK.h>

NS_ASSUME_NONNULL_BEGIN

#define TAPFRIEND_VERSION_NUMBER @"1"
#define TAPFRIEND_VERSION        @"1.0.0"

typedef void (^TapSimpleHandler)(NSError *_Nullable error);
typedef void (^TapFriendUserInfoMultiHandler)(NSArray<TapUserRelationShip *> *_Nullable userList, NSError *_Nullable error);
typedef void (^TapFriendRelationHandler)(NSArray<TapUserRelationShip *> *_Nullable userList, NSError *_Nullable error);

@interface TapFriends : NSObject

/// 初始化
/// @param config config
/// @param provider provider
+ (void)initWithConfig:(TapConfig *)config accountProvider:(id<TDSAccountProvider>)provider;

/// 添加好友
/// @param userId 用户tds_id
/// @param handler 回调
+ (void)addFriend:(NSString *)userId
          handler:(TapSimpleHandler)handler;

/// 删除好友
/// @param userId 用户tds_id
/// @param handler 回
+ (void)deleteFriend:(NSString *)userId
             handler:(TapSimpleHandler)handler;

/// 获取关注列表
/// @param from 起始index
/// @param mutualAttention 是否互相关注 0:不是互相关注 1: 互相关注
/// @param limit 单页个数限制
/// @param handler 回调
+ (void)getFollowingList:(NSInteger)from
         mutualAttention:(BOOL)mutualAttention
                   limit:(NSInteger)limit
                 handler:(TapFriendRelationHandler)handler;

/// 获取粉丝列表
/// @param from 起始index
/// @param limit 单页个数限制
/// @param handler 回
+ (void)getFollowerList:(NSInteger)from
                  limit:(NSInteger)limit
                handler:(TapFriendRelationHandler)handler;

/// 拉黑用户
/// @param userId 用户tds_id
/// @param handler 回
+ (void)blockUser:(NSString *)userId
          handler:(TapSimpleHandler)handler;

/// 取消拉黑用户
/// @param userId 用户tds_id
/// @param handler 回
+ (void)unblockUser:(NSString *)userId
            handler:(TapSimpleHandler)handler;

/// 黑名单列表
/// @param from 起始index
/// @param limit 单页个数限制
/// @param handler 回
+ (void)getBlockList:(NSInteger)from
               limit:(NSInteger)limit
             handler:(TapFriendRelationHandler)handler;

@end

NS_ASSUME_NONNULL_END
