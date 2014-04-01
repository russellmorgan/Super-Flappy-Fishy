////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InAppPurchaseManager : EventDispatcher {


	public const string APPLE_VERIFICATION_SERVER   = "https://buy.itunes.apple.com/verifyReceipt";
	public const string SANDBOX_VERIFICATION_SERVER = "https://sandbox.itunes.apple.com/verifyReceipt";



	public const string PRODUCT_BOUGHT 		= "product_bought";
	public const string TRANSACTION_FAILED 	= "transaction_failed";
	public const string RESTORE_TRANSACTION_FAILED 	= "restore_transaction_failed";

	public const string VERIFICATION_RESPONCE 	= "verification_responce";
	public const string STORE_KIT_INITIALIZED	    = "store_kit_initialized";
	
	
	public bool isStoreLoaded = false;
	
	private List<string> _productsIds =  new List<string>();
	private List<ProductTemplate> _products    =  new List<ProductTemplate>();
	
	
	private static InAppPurchaseManager _instance;
	
	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public static InAppPurchaseManager instance {
		get {
			if(_instance ==  null) {
				GameObject go =  new GameObject("InAppPurchaseManager");
				DontDestroyOnLoad(go);
				_instance =  go.AddComponent<InAppPurchaseManager>();
			}
			
			return _instance;
		}
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void loadStore() {
		string ids = "";
		int len = _productsIds.Count;
		for(int i = 0; i < len; i++) {
			if(i != 0) {
				ids += ",";
			}
			
			ids += _productsIds[i];
		}
		
		IOSNative.loadStore(ids);
		
	}
	
	public void buyProduct(string productId) {
		if(!isStoreLoaded) {

			IOSStoreKitResponce responce = new IOSStoreKitResponce ();
			responce.productIdentifier = productId;
			responce.error =  "Store kit not yet initialized";

			Debug.LogWarning("buyProduct shouldn't be called before store kit initialized"); 
			
			dispatch(TRANSACTION_FAILED, responce);

			return;
		}

		IOSNative.buyProduct(productId);
	}
	
	public void addProductId(string productId) {
		_productsIds.Add(productId);
	}
	
	public void restorePurchases() {

		if(!isStoreLoaded) {
			dispatch(RESTORE_TRANSACTION_FAILED);
			return;
		}

		IOSNative.restorePurchases();
	}

	public void verifyLastPurchase(string url) {
		IOSNative.verifyLastPurchase (url);
	}
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public List<ProductTemplate> products {
		get {
			return _products;
		}
	}
	
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	public void onStoreDataRecived(string data) {
		if(data.Equals(string.Empty)) {
			Debug.Log("InAppPurchaseManager, no products avaiable: " + _products.Count.ToString());
			return;
		}


		string[] storeData;
		storeData = data.Split("|" [0]);
		
		for(int i = 0; i < storeData.Length; i+=5) {
			ProductTemplate tpl =  new ProductTemplate();
			tpl.id 				= storeData[i];
			tpl.title 			= storeData[i + 1];
			tpl.description 	= storeData[i + 2];
			tpl.localizedPrice 	= storeData[i + 3];
			tpl.price 			= storeData[i + 4];
			_products.Add(tpl);
		}
		
		Debug.Log("InAppPurchaseManager, tottal products loaded: " + _products.Count.ToString());
		isStoreLoaded = true;
		dispatch (STORE_KIT_INITIALIZED);
	}
	
	public void onProductBought(string array) {

		string[] data;
		data = array.Split("|" [0]);
		IOSStoreKitResponce responce = new IOSStoreKitResponce ();
		responce.productIdentifier = data [0];
		responce.receipt = data [1];


		dispatch(PRODUCT_BOUGHT, responce);
	}
	
	public void onTransactionFailed(string array) {

		string[] data;
		data = array.Split("|" [0]);

		IOSStoreKitResponce responce = new IOSStoreKitResponce ();
		responce.productIdentifier = data [0];
		responce.error =  data [1];


		dispatch(TRANSACTION_FAILED, responce);
	}
	
	
	public void onVerificationResult(string array) {

		string[] data;
		data = array.Split("|" [0]);

		IOSStoreKitVerificationResponce responce = new IOSStoreKitVerificationResponce ();
		responce.status = System.Convert.ToInt32(data[0]);
		responce.orijinalJSON = data [1];
		responce.receipt = data [2];

		dispatch (VERIFICATION_RESPONCE, responce);

	}

	public void onRestoreTransactionFailed(string array) {
		dispatch(RESTORE_TRANSACTION_FAILED);
	}
	


	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
