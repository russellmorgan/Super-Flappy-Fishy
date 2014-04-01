//
//  InAppPurchaseManager.h
//
//  Created by Osipov Stanislav on 1/15/13.
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>


#import "TransactionServer.h"

#define kInAppPurchaseManagerProductsFetchedNotification @"kInAppPurchaseManagerProductsFetchedNotification"

@interface InAppPurchaseManager : NSObject <SKProductsRequestDelegate> {
    NSMutableArray * _productIdentifiers;
    NSMutableDictionary * _products;
    TransactionServer * _storeServer;
    
   
}

+ (InAppPurchaseManager *) instance;

- (void)loadStore;
- (void)restorePurchases;
- (void)addProductId:(NSString *) productId;
- (void)buyProduct:(NSString * )productId;


-(void) verifyLastPurchase:(NSString *) verificationURL;


@end
