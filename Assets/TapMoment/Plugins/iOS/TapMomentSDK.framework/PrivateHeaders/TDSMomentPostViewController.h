//
//  TDSMomentPostViewController.h
//  TapMoment
//
//  Created by ritchie on 2020/7/20.
//  Copyright © 2020 TapTap. All rights reserved.
//

#import "TDSMomentWebViewController.h"
#import "TapMoment.h"
#import "TDSMomentAccessToken.h"

NS_ASSUME_NONNULL_BEGIN

@class TapMomentPostData;

@interface TDSMomentPostViewController : TDSMomentWebViewController


- (instancetype)init NS_UNAVAILABLE;
- (instancetype)new NS_UNAVAILABLE;

- (instancetype)initWithContent:(TapMomentPostData *)content
                    accessToken:(TDSMomentAccessToken *)accessToken
                         config:(TapMomentConfig *)config;

@end

NS_ASSUME_NONNULL_END
