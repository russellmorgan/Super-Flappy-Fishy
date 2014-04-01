////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCenterExample : MonoBehaviour {
	
	public int hiScore = 100;
	
	private string leaderBoardId =  "your.leaderbord1.id.here";
	private string leaderBoardId2 = "your.leaderbord2.id.here";


	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";
	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";



	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	void Awake() {

		IOSNative.instance.Init();

		//Achievement registration. If you will skipt this step GameCenterManager.achievements array will contain only achievements with reported progress 
		GameCenterManager.registerAchievement (TEST_ACHIEVEMENT_1_ID);
		GameCenterManager.registerAchievement (TEST_ACHIEVEMENT_2_ID);


		//Listen for the Game Center events
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENTS_LOADED, OnAchievementsLoaded);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENT_PROGRESS, OnAchievementProgress);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_ACHIEVEMENTS_RESET, OnAchievementsReset);


		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_LEADER_BOARD_SCORE_LOADED, OnLeaderBoarScoreLoaded);

		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_PLAYER_AUTHENTICATED, OnAuth);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED, OnAuthFailed);

		DontDestroyOnLoad (gameObject);
		
		
		//Initializing Game Cneter class. This action will triger authentication flow
		GameCenterManager.init();
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	void OnGUI() {
		if(GUI.Button(new Rect(330, 10, 150, 50), "Show Leader Board1")) {
			GameCenterManager.showLeaderBoard(leaderBoardId);
		}


		
		if(GUI.Button(new Rect(330, 70, 150, 50), "Report Score LB 1")) {
			hiScore++;
			GameCenterManager.reportScore(1029, leaderBoardId);
		}

		if(GUI.Button(new Rect(330, 130, 150, 50), "Get Score LB 1")) {
			GameCenterManager.getScore(leaderBoardId);
		}

		if(GUI.Button(new Rect(330, 190, 150, 50), "Show Leader Board2")) {
			GameCenterManager.showLeaderBoard(leaderBoardId2);
		}


		if(GUI.Button(new Rect(330, 250, 150, 50), "Report Score LB2")) {
			hiScore++;
			GameCenterManager.reportScore(hiScore, leaderBoardId2);
		}

		if(GUI.Button(new Rect(330, 310, 150, 50), "Get Score LB 2")) {
			GameCenterManager.getScore(leaderBoardId2);
		}


		if(GUI.Button(new Rect(330, 370, 150, 50), "Show Leaderboards")) {
			GameCenterManager.showLeaderBoards ();
		}




		if(GUI.Button(new Rect(490, 10, 150, 50), "Show Achievements")) {
			GameCenterManager.showAchievements();
		}


		if(GUI.Button(new Rect(490, 70, 150, 50), "Reset Achievements")) {
			GameCenterManager.resetAchievements();
		}


		if(GUI.Button(new Rect(490, 130, 150, 50), "Submit Achievements1")) {
			GameCenterManager.submitAchievement(GameCenterManager.getAchievementProgress(TEST_ACHIEVEMENT_1_ID) + 2.432f, TEST_ACHIEVEMENT_1_ID);
		}

		if(GUI.Button(new Rect(490, 190, 150, 50), "Submit Achievements2")) {
			GameCenterManager.submitAchievement(88.66f, TEST_ACHIEVEMENT_2_ID, false);
		}
		


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
	
	private void OnAuthFailed() {
		IOSNative.showMessage("Game Cneter ", "Player auntification failed");
		
		//if you got this event it means that player canseled auntification flow. With probably mean that playr do not whant to use gamcenter in your game
		
	
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
