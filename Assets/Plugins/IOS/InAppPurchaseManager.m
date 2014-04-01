//
//  InAppPurchaseManager.m
//
//  Created by Osipov Stanislav on 1/15/13.
//

#import "InAppPurchaseManager.h"
#import "Unity3d.h"

#import "SKProduct+LocalizedPrice.h"
#include "iAdBannerController.h"

@implementation InAppPurchaseManager

static InAppPurchaseManager * _instance;

+ (InAppPurchaseManager *) instance {
    
    if (_instance == nil){
        _instance = [[InAppPurchaseManager alloc] init];
    }
    
    return _instance;
}

-(id) init {
    NSLog(@"init");
    if(self = [super init]){
        _productIdentifiers = [[NSMutableArray alloc] init];
        _products           = [[NSMutableDictionary alloc] init];
        
        _storeServer        = [[TransactionServer alloc] init];
        [[SKPaymentQueue defaultQueue] addTransactionObserver:_storeServer];
    }
    return self;
}

-(void) dealloc {
    [_productIdentifiers release];
    [_storeServer release];
    [super dealloc];
}


- (void)loadStore {
    NSLog(@"loadStore....");
    SKProductsRequest *request= [[SKProductsRequest alloc] initWithProductIdentifiers:[NSSet setWithArray:_productIdentifiers]];

    request.delegate = self;
    [request start];
}


-(void) addProductId:(NSString *)productId {
    [_productIdentifiers addObject:productId];
}


- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response {
    NSLog(@"productsRequest....");
    NSLog(@"Total loaded products: %i", [response.products count]);
    
    
    NSMutableString * data = [[NSMutableString alloc] init];
    BOOL first = YES;
    
    for (SKProduct *product in response.products) {
        [_products setObject:product forKey:product.productIdentifier];
        
        if(!first) {
            [data appendString:@"|"];
        }
        
        [data appendString:product.productIdentifier];
        [data appendString:@"|"];
        
        [data appendString:product.localizedTitle];
        [data appendString:@"|"];
        
        [data appendString:product.localizedDescription];
        [data appendString:@"|"];
        
        [data appendString:product.localizedPrice];
        [data appendString:@"|"];
        
        
        [data appendString:[product.price stringValue]];
        first = NO;
    }
    
    for (NSString *invalidProductId in response.invalidProductIdentifiers) {
        NSLog(@"Invalid product id: %@" , invalidProductId);
    }
    
    
    UnitySendMessage("InAppPurchaseManager", "onStoreDataRecived", [Unity3d NSStringToChar:data]);
    [[NSNotificationCenter defaultCenter] postNotificationName:kInAppPurchaseManagerProductsFetchedNotification object:self userInfo:nil];
    
}

-(void) restorePurchases {
    [[SKPaymentQueue defaultQueue] addTransactionObserver:_storeServer];
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}

-(void) buyProduct:(NSString *)productId {
    
    
    if ([SKPaymentQueue canMakePayments]) {
        SKProduct* selectedProduct = [_products objectForKey: productId];
        if(selectedProduct != NULL) {
            SKPayment *payment = [SKPayment paymentWithProduct:selectedProduct];
            [[SKPaymentQueue defaultQueue] addPayment:payment];
        } else {
            UIAlertView *alert = [[UIAlertView alloc] init];
            [alert setTitle:@"Connection Error"];
            [alert setMessage:@"Cannot connect to payment servers, check your internet connection"];
            [alert addButtonWithTitle:@"Ok"];
            [alert setDelegate:NULL];
            [alert show];
            [alert release];
        }
    } else {
        UIAlertView *alert = [[UIAlertView alloc] init];
        [alert setTitle:@"In-App Purchase's disaled"];
        [alert setMessage:@"Check settings to allow In-App Purchase's on this device "];
        [alert addButtonWithTitle:@"Ok"];
        [alert setDelegate:NULL];
        [alert show];
        [alert release];
    }
}

-(void) verifyLastPurchase:(NSString *) verificationURL {
    [_storeServer verifyLastPurchase:verificationURL];
}


@end
