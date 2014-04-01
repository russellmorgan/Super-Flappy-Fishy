//
//  Unity3d.m
//
//  Created by Osipov Stanislav on 1/11/13.
//
//

#import "Unity3d.h"

@implementation Unity3d

+(NSString *) charToNSString:(char *)value {
    if (value != NULL) {
        return [NSString stringWithUTF8String: value];
    } else {
        return [NSString stringWithUTF8String: ""];
    }
}

+(const char *)NSIntToChar:(NSInteger)value {
    NSString *tmp = [NSString stringWithFormat:@"%d", value];
    return [tmp UTF8String];
}

+ (const char *)NSStringToChar:(NSString *)value {
    return [value UTF8String];
}




@end


