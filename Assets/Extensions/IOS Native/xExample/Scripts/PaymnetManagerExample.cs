////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaymnetManagerExample {
	
	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public const string SMALL_PACK 	=  "your.product.id1.here";
	public const string NC_PACK 	=  "your.product.id2.here";


	public static void init() {

		InAppPurchaseManager.instance.addProductId(SMALL_PACK);
		InAppPurchaseManager.instance.addProductId(NC_PACK);
		
		InAppPurchaseManager.instance.addEventListener(InAppPurchaseManager.PRODUCT_BOUGHT, onProductBought);
		InAppPurchaseManager.instance.addEventListener(InAppPurchaseManager.TRANSACTION_FAILED, onTransactionFailed);
		InAppPurchaseManager.instance.addEventListener(InAppPurchaseManager.RESTORE_TRANSACTION_FAILED, onRestoreTransactionFailed);
		InAppPurchaseManager.instance.addEventListener(InAppPurchaseManager.VERIFICATION_RESPONCE, onVerificationResponce);

		InAppPurchaseManager.instance.loadStore();
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	
	public static void buyItem(string productId) {
		InAppPurchaseManager.instance.buyProduct(productId);
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	private static void onProductBought(CEvent e) {
		
		IOSStoreKitResponce responce =  e.data as IOSStoreKitResponce;
		Debug.Log("STORE KIT GOT BUY: " + responce.productIdentifier);
		Debug.Log("RECIPT: " + responce.receipt);

		switch(responce.productIdentifier) {
		case SMALL_PACK:
			//code for adding small game money amount here
			break;
		case NC_PACK:
			//code for unlocking cool item here
			break;

		}

		
		IOSNative.showMessage("Success", "product " + responce.productIdentifier + " is purchased");
	}

	private static void onRestoreTransactionFailed() {
		IOSNative.showMessage("Fail", "Restore Failed");
	}

	private static void onTransactionFailed(CEvent e) {
		IOSStoreKitResponce responce =  e.data as IOSStoreKitResponce;
		IOSNative.showMessage("Fail", responce.error);
	}

	private static void onVerificationResponce(CEvent e) {
		IOSStoreKitVerificationResponce responce =  e.data as IOSStoreKitVerificationResponce;

		IOSNative.showMessage("Verification", "Transaction verification status: " + responce.status.ToString());

		Debug.Log("ORIJINAL JSON ON: " + responce.orijinalJSON);
	}


	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
