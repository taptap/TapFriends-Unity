//
//  TDSSDKForumWebViewController.h
//  TapTapSDK
//
//  Created by sunyi on 2018/1/2.
//  Copyright © 2018年 易玩. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "TDSMomentWKWebViewJavascriptBridge.h"
#import "TapMoment.h"
#import "TDSMomentAccessToken.h"

@protocol TDSSDKForumWebViewDelegate <NSObject>
- (void)onWebviewResult:(NSString *)result;
@end

@interface TDSMomentWebViewController : UIViewController

@property (weak, nonatomic) id<TapMomentDelegate> delegate;
@property (weak, nonatomic) id<TDSSDKForumWebViewDelegate> resultDelegate;
@property (nonatomic, strong) TDSMomentAccessToken *accessToken;
@property (nonatomic, strong) TapMomentConfig *config;
@property (nonatomic, strong) TDSMomentWKWebViewJavascriptBridge *bridge;
@property (nonatomic, copy) NSString *clientId;
@property (nonatomic, copy) NSString *version;
@property (nonatomic, copy) NSString *customUA;
@property (nonatomic, copy) NSString *URL;
@property (nonatomic, copy) NSString *urlExtra;

- (void)dismissSelf;

@end
