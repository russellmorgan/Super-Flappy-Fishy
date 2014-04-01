using UnityEngine;
using System.Collections;

public class LeaderBoardScoreData  {

	public string leaderBoardId;

	public string leaderBoardScore;


	public float GetFloatScore() {
		return System.Convert.ToSingle (leaderBoardScore);
	}

	public int GetIntScore() {
		return System.Convert.ToInt32 (leaderBoardScore);
	}
}
