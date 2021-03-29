//
//  TDSMomentAPI.h
//  TDSMoment
//
//  Created by Bottle K on 2021/1/7.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TDSMomentAPI : NSObject
@property (nonatomic, assign) BOOL isCN;

+ (instancetype)shareInstance;

+ (NSString *)getWebHost;

+ (NSString *)getServerHost;
@end

NS_ASSUME_NONNULL_END
