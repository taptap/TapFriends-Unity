//
//  TDSMomentMacros.h
//  TapTapSDK
//
//  Created by 孙毅 on 2018/11/27.
//  Copyright © 2018 taptap. All rights reserved.
//

#import <TapCommonSDK/NSBundle+Tools.h>

#define MOMENT_IMAGE(key) [[NSBundle tds_bundleName:@"TapMomentResource" aClass:[self class]] tds_imageName:key]

