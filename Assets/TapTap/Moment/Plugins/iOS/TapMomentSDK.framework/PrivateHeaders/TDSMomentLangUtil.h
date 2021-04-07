//
//  TDSMomentLangUtil.h
//  TDSMoment
//
//  Created by Bottle K on 2021/3/19.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TDSMomentLangUtil : NSObject
+ (instancetype)sharedManager;

- (NSString *)getTranslatedString:(NSString *)key;
@end

NS_ASSUME_NONNULL_END
