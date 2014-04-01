using UnityEngine;
using System.Collections;

public class JSHelper : MonoBehaviour {
	
	private string leaderBoardId =  "your.leaderbord1.id.here";


	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";
	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	void Awake() {
		IOSNative.instance.Init();
	}


	void InitGameCneter() {


		//Achievement registration. If you will skipt this step GameCenterManager.achievements array will contain only achievements with reported progress 
		GameCenterManager.registerAchievement (TEST_ACHIEVEMENT_1_ID);
		GameCenterManager.registerAchievement (TEST_ACHIEVEMENT_2_ID);


		//Listen for the Game Center events
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENTS_LOADED, OnAchievementsLoaded);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENT_PROGRESS, OnAchievementProgress);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENTS_RESET, OnAchievementsReset);


		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_LEADER_BOARD_SCORE_LOADED, OnLeaderBoarScoreLoaded);

		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_PLAYER_AUTHENTICATED, OnAuth);

		DontDestroyOnLoad (gameObject);

		GameCenterManager.init();
		
		Debug.Log("InitGameCneter");
	}
	
	
	private void SubmitScore(int val) {
		Debug.Log("SubmitScore");
		GameCenterManager.reportScore(val, leaderBoardId);
	}
	
	private void SubmitAchievement(string data) {
		
		string[] arr;
		arr = data.Split("|" [0]);
		
		float percent = System.Convert.ToSingle(arr[0]);
		string achievementId = arr[1];
		
		
		
		Debug.Log("SubmitAchievement: " + achievementId + "  " + percent.ToString());
		GameCenterManager.submitAchievement(percent, achievementId);
	}
	
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void OnAchievementsLoaded() {
		Debug.Log ("Achievemnts was loaded from IOS Game Center");

		foreach(AchievementTemplate tpl in GameCenterManager.achievements) {
			Debug.Log (tpl.id + ":  " + tpl.progress);
		}
	}

	private void OnAchievementsReset() {
		Debug.Log ("All  Achievemnts was reseted");
	}

	private void OnAchievementProgress(CEvent e) {
		Debug.Log ("OnAchievementProgress");

		AchievementTemplate tpl = e.data as AchievementTemplate;
		Debug.Log (tpl.id + ":  " + tpl.progress.ToString());
	}

	private void OnLeaderBoarScoreLoaded(CEvent e) {
		LeaderBoardScoreData data = e.data as LeaderBoardScoreData;
		IOSNative.showMessage("Leader Board " + data.leaderBoardId, "Score: " + data.leaderBoardScore);
	}


	private void OnAuth() {
		IOSNative.showMessage("Player Authed ", "ID: " + GameCenterManager.player.playerId + "\n" + "Name: " + GameCenterManager.player.displayName);
	}
	
}
