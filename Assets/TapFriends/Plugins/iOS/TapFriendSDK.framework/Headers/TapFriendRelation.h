//
//  TapFriendRelation.h
//  TDSSDK
//
//  Created by Bottle K on 2021/3/12.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TapFriendRelation : NSObject
@property (nonatomic, copy) NSString *userId;
@property (nonatomic, copy, nullable) NSString *name;
@property (nonatomic, copy, nullable) NSString *avatar;
@property (nonatomic, assign) BOOL isGuest;
@property (nonatomic, assign) long gender;
@property (nonatomic, assign) NSInteger mutualFans;
@end

NS_ASSUME_NONNULL_END
