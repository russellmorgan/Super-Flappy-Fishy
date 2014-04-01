////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GameCenterManager : MonoBehaviour {


	public const string GAME_CENTER_PLAYER_AUTHENTICATED       		= "game_center_player_authenticated";
	public const string GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED  = "game_center_player_authentification_failed";
	public const string GAME_CENTER_ACHIEVEMENTS_RESET        	 	= "game_center_achievements_reset";
	public const string GAME_CENTER_ACHIEVEMENTS_LOADED        		= "game_center_achievements_loaded";
	public const string GAME_CENTER_LEADER_BOARD_SCORE_LOADED  		= "game_center_leader_board_score_loaded";


	public const string GAME_CENTER_ACHIEVEMENT_PROGRESS  = "game_center_achievement_progress";

	
	[DllImport ("__Internal")]
	private static extern void _initGamaCenter();
	
	[DllImport ("__Internal")]
	private static extern void _showLeaderBoard(string leaderBoradrId);

	[DllImport ("__Internal")]
	private static extern void _reportScore (int score, string leaderBoradrId);



	[DllImport ("__Internal")]
	private static extern void _showLeaderBoards ();


	[DllImport ("__Internal")]
	private static extern void _getLeadrBoardScore (string leaderBoradrId);
	
	[DllImport ("__Internal")]
	private static extern void _showAchievements();

	[DllImport ("__Internal")]
	private static extern void _resetAchievements();
	

	[DllImport ("__Internal")]
	private static extern void _submitAchievement(float percent, string achievementId, bool isCompleteNotification);



	private  static bool _IsInited = false;
	private  static bool _IsPlayerAuthed = false;


	private static List<AchievementTemplate> _achievements = new List<AchievementTemplate> ();
	private static EventDispatcherBase _dispatcher  = new EventDispatcherBase ();


	private static GameCenterPlayerTemplate _player = null;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public static void init() {

		if(_IsInited) {
			return;
		}

		_IsInited = true;


		GameObject go =  new GameObject("GameCenterManager");
		go.AddComponent<GameCenterManager>();
		DontDestroyOnLoad(go);


		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_initGamaCenter();
		}
			
	}
	



	public static void registerAchievement(string achievemenId) {
		bool isContains = false;

		foreach(AchievementTemplate t in _achievements) {
			if (t.id.Equals (achievemenId)) {
				isContains = true;
			}
		}


		if(!isContains) {
			AchievementTemplate tpl = new AchievementTemplate ();
			tpl.id = achievemenId;
			tpl.progress = 0;
			_achievements.Add (tpl);
		}
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	

	
	public static void showLeaderBoard(string leaderBoradrId) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showLeaderBoard(leaderBoradrId);
		}
	}

	public static void showLeaderBoards() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showLeaderBoards ();
		}
	}
	

	public static void reportScore(int score, string leaderBoradrId) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_reportScore(score, leaderBoradrId);
		}
	}

	public static void getScore(string leaderBoradrId) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_getLeadrBoardScore(leaderBoradrId);
		}
	}



	public static void showAchievements() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showAchievements();
		}
	}

	public static void resetAchievements() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_resetAchievements();

			foreach(AchievementTemplate tpl in _achievements) {
				tpl.progress = 0f;
			}
		}
	}


	public static void submitAchievement(float percent, string achievementId) {
		submitAchievement (percent, achievementId, true);
	}

	public static void submitAchievement(float percent, string achievementId, bool isCompleteNotification) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_submitAchievement(percent, achievementId, isCompleteNotification);
		}
	}


	public static float getAchievementProgress(string id) {
		float progress = 0f;
		foreach(AchievementTemplate tpl in _achievements) {
			if(tpl.id == id) {
				return tpl.progress;
			}
		}

		return progress;
	}
	

	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public static List<AchievementTemplate> achievements {
		get {
			return _achievements;
		}
	}

	public static EventDispatcherBase dispatcher {
		get {
			return _dispatcher;
		}
	}

	public static GameCenterPlayerTemplate player {
		get {
			return _player;
		}
	}


	private static bool IsInited {
		get {
			return _IsInited;
		}
	}


	private static bool IsPlayerAuthed {
		get {
			return _IsPlayerAuthed;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------


	private void onLeaderBoardScore(string array) {

		string[] data;
		data = array.Split("," [0]);
		LeaderBoardScoreData lData = new LeaderBoardScoreData ();
		lData.leaderBoardId = data [0];
		lData.leaderBoardScore = data [1];

		dispatcher.dispatch (GAME_CENTER_LEADER_BOARD_SCORE_LOADED, lData);
	}



	private void onAchievementsReset(string array) {
		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_RESET);
	}


	private void onAchievementProgressChanged(string array) {
		string[] data;
		data = array.Split("," [0]);

		AchievementTemplate tpl =  new AchievementTemplate();
		tpl.id = data [0];
		tpl.progress = System.Convert.ToSingle(data [1]) ;


		submitAchievement (tpl);

		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENT_PROGRESS, tpl);

	}


	private void onAchievementsLoaded(string array) {

		if(array.Equals(string.Empty)) {
			dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_LOADED);
			return;
		}

		string[] data;
		data = array.Split("," [0]);


		for(int i = 0; i < data.Length; i+=2) {
			AchievementTemplate tpl =  new AchievementTemplate();
			tpl.id 				= data[i];
			tpl.progress 		= System.Convert.ToSingle(data[i + 1]);
			submitAchievement (tpl);
		}

		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_LOADED);
	}

	private void onAuthenticateLocalPlayer(string  array) {
		string[] data;
		data = array.Split("," [0]);

		_player = new GameCenterPlayerTemplate (data[0], data [1]);

		_IsPlayerAuthed = true;
		dispatcher.dispatch (GAME_CENTER_PLAYER_AUTHENTICATED);
	}
	
	
	private void onAuthenticationFailed(string  array) {
		_IsPlayerAuthed = false;
		dispatcher.dispatch(GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	private void submitAchievement(AchievementTemplate tpl) {
		bool isContains = false;
		foreach(AchievementTemplate t in _achievements) {
			if (t.id.Equals (tpl.id)) {
				isContains = true;
				t.progress = tpl.progress;
			}
		}

		if(!isContains) {
			_achievements.Add (tpl);
		}
	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
