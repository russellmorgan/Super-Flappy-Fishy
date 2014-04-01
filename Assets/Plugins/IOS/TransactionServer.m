//
//  TransactionServer.m
//
//  Created by Osipov Stanislav on 1/16/13.
//

#import "TransactionServer.h"
#import "Unity3d.h"



@implementation TransactionServer

NSString* lastTransaction = @"";

- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions {
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
            default:
                break;
        }
    }
}


-(void) verifyLastPurchase:(NSString *)verificationURL {
    NSLog(@"url: %@",verificationURL);
    
    
    NSURL *url = [NSURL URLWithString:verificationURL];
    NSMutableURLRequest *theRequest = [NSMutableURLRequest requestWithURL:url];
    
   // NSString *st =  lastTransaction;
    
    
    NSString *json = [NSString stringWithFormat:@"{\"receipt-data\":\"%@\"}", lastTransaction];
    
    [theRequest setHTTPBody:[json dataUsingEncoding:NSUTF8StringEncoding]];
    [theRequest setHTTPMethod:@"POST"];
    [theRequest setValue:@"application/x-www-form-urlencoded" forHTTPHeaderField:@"Content-Type"];
    NSString *length = [NSString stringWithFormat:@"%d", [json length]];
    [theRequest setValue:length forHTTPHeaderField:@"Content-Length"];
    NSHTTPURLResponse* urlResponse = nil;
    NSError *error = [[NSError alloc] init];
    NSData *responseData = [NSURLConnection sendSynchronousRequest:theRequest
                                                 returningResponse:&urlResponse
                                                             error:&error];
    NSString *responseString = [[NSString alloc] initWithData:responseData encoding:NSUTF8StringEncoding];
    
  //  NSLog(@"resp: %@",responseString);
    
    NSError *e = nil;
    
    NSDictionary *dic =
    [NSJSONSerialization JSONObjectWithData: [responseString dataUsingEncoding:NSUTF8StringEncoding]
                                    options: NSJSONReadingMutableContainers
                                      error: &e];
    
    NSString *statusCode = [NSString stringWithFormat:@"%d", [[dic objectForKey:@"status"] intValue]];


    
    NSMutableString * data = [[NSMutableString alloc] init];
    
    [data appendString:statusCode];
    [data appendString:@"|"];
    [data appendString: responseString];
    [data appendString:@"|"];
    [data appendString: lastTransaction];
    
    NSString *str = [[data copy] autorelease];
    UnitySendMessage("InAppPurchaseManager", "onVerificationResult", [Unity3d NSStringToChar:str]);

}



- (NSString *)encodeBase64:(const uint8_t *)input length:(NSInteger)length
{
    NSData * data = [NSData dataWithBytes:input length:length];
    return [data base64EncodedString];
}


- (NSString *)getReceipt:(SKPaymentTransaction *)transaction {
    NSString *Receipt =  [self encodeBase64:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    return Receipt;
}



- (void)provideContent:(SKPaymentTransaction *)transaction {
    
    NSLog(@"provideContent for: %@", transaction.payment.productIdentifier);

    lastTransaction = [self encodeBase64:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    
    NSMutableString * data = [[NSMutableString alloc] init];
    
    [data appendString:transaction.payment.productIdentifier];
    [data appendString:@"|"];
    [data appendString: [self getReceipt:transaction]];
    
    NSString *str = [[data copy] autorelease];
    UnitySendMessage("InAppPurchaseManager", "onProductBought", [Unity3d NSStringToChar:str]); 

    
}


- (void)completeTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"completeTransaction...");
    
    [self provideContent:transaction];
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
}

- (void)restoreTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"restoreTransaction...");
    
    [self provideContent:transaction];
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
}

- (void)failedTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"Transaction Failed with code : %i", transaction.error.code);
    NSLog(@"Transaction error: %@", transaction.error.localizedDescription);
    
    if(transaction.error.code != SKErrorPaymentCancelled) {
        
        UIAlertView *alert = [[UIAlertView alloc] init];
        [alert setTitle:@"Transaction Error"];
        [alert setMessage:transaction.error.localizedDescription];
        [alert addButtonWithTitle:@"Ok"];
        [alert setDelegate:NULL];
        [alert show];
        [alert release];
    }
    
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
    
    NSMutableString * data = [[NSMutableString alloc] init];
    
    
    [data appendString:transaction.payment.productIdentifier];
    [data appendString:@"|"];
    [data appendString:transaction.error.localizedDescription];
    
    NSString *str = [[data copy] autorelease];
    UnitySendMessage("InAppPurchaseManager", "onTransactionFailed", [Unity3d NSStringToChar:str]);
    
    
    
}


- (void)paymentQueue:(SKPaymentQueue *)queue restoreCompletedTransactionsFailedWithError:(NSError *)error {
    NSLog(@"%@",error);
    UnitySendMessage("InAppPurchaseManager", "onRestoreTransactionFailed", [Unity3d NSStringToChar:@""]);
}

- (void) paymentQueueRestoreCompletedTransactionsFinished:(SKPaymentQueue *)queue
{
    NSLog(@"received restored transactions: %i", queue.transactions.count);
    
    for (SKPaymentTransaction *transaction in queue.transactions)
    {
        NSString *productID = transaction.payment.productIdentifier;
        
        NSLog(@"%@",productID);
    }
}



@end
