using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreGenerator : MonoBehaviour {

	public static string jsonData = File.ReadAllText(@"Assets/crickData.json");
	public TeamList teams = JsonUtility.FromJson<TeamList>(jsonData);

	public static string jsonData1 = File.ReadAllText(@"Assets/jsonSettings.txt");
	public jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData1);

	public scoreBoardManager sbm;

	public int minScore = 30;
	public int maxScore = 60;
	public int extras = 0;

	public GameObject mainObj;
	globalMatchManager gm;
	// Use this for initialization


	void Start () {

		//TODO: Actual code

		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();

		//setTarget (json.myTeam,json.opponentTeam);

	}

	public List<int> populateAutoPlayScore(int target)
	{
		int extraRuns = Random.Range (0, 4);
		int divisions = Random.Range (2, 7);
		target = target - extraRuns;
		extras = extraRuns;
		//Random rand = new Random();
		int ducks = Random.Range(-1,divisions-2);
		int randDuck;
		List<int>duckPositions = new List<int>();
		for(int j = 0;j<ducks;)
		{
			randDuck = Random.Range(0,divisions);		
			if (!duckPositions.Contains (randDuck)) {
				duckPositions.Add(randDuck);
				j++;
			}
		}
		int[] div = new int[divisions];
		List<int>container = new List<int>(div);
		int randPlayer;

		while (target > 0)
		{
			randPlayer = Random.Range (0, divisions);
			if (!duckPositions.Contains (randPlayer)) {
				container [randPlayer]++;
				target--;
			}
		}
		Debug.Log (container);
		for (int j = 0; j < ducks; j++) {
			container [duckPositions [j]] = 0;
		}
		return container;
		
	}

	int computeScoreByRatings(int yourTeamRating, int oppTeamRating, int minRate, int MaxRate)
	{
		
		int minDiff = 5, maxDiff = MaxRate-minRate;
		int mainDiff = Mathf.Abs(yourTeamRating - oppTeamRating);
		float target = ((float)(mainDiff - minDiff) / (float)(maxDiff - minDiff)) * (maxScore - minScore);
		target = target + minScore;
		return (int)target;
	}

	public int setTarget(string yourTeam, string oppTeam)
	{
		teams = JsonUtility.FromJson<TeamList>(jsonData);
		json = JsonUtility.FromJson<jsonConfig>(jsonData1);
		if (json.Overs == "2") {
			minScore = 15;
			maxScore = 25;

		} else if (json.Overs == "1") {
			minScore = 10;
			maxScore = 15;
		}
		int totTeams = teams.Teams.Count;
		List<TeamInfo> teamsInfo = teams.Teams;
		int yourTeamRating=0, oppTeamRating=0;
		int minRate=100000, maxRate=0;
		for (int i = 0; i < totTeams; i++) {
			
			int teamRating = int.Parse (teamsInfo [i].team_rating);

			if (maxRate < teamRating)
				maxRate = teamRating;
			
			if (minRate > teamRating)
				minRate = teamRating;
			
			if (teamsInfo [i].team == yourTeam)
				yourTeamRating = teamRating;
			else if (teamsInfo [i].team == oppTeam)
				oppTeamRating = teamRating;
		}

		int genScore = computeScoreByRatings (yourTeamRating, oppTeamRating,minRate,maxRate);
		int lowerBound = genScore - 5;
		int higherBoundBound = genScore + 5;
		int finalTarget = Random.Range(lowerBound,higherBoundBound);
		//populateAutoPlayScore (finalTarget);
		sbm = new scoreBoardManager ();
		sbm.SetTargetText (finalTarget.ToString());
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		gm.target = finalTarget;
		return finalTarget;
	}


	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class Players
{
	public string name;
	public string batsman_innings;
	public string sixes;
	public string hs;
	public string tenW;
	public string captain;
	public string mom;
	public string mos;
	public string fourW;
	public string threeW;
	public string matches;
	public string hundreds;
	public string bowler_wickets;
	public string bowler_conceded;
	public string bowler_maidens;
	public string fifties;
	public string fours;
	public string bowler_innings;
	public string avg;
	public string economy;
	public string fiveW;
	public string sr;
	public string batsman_runs;
}

[System.Serializable]
public class TeamInfo
{
	public string team;
	public string team_rating;
	public string captain;
	public string WicketKeeper;
	public string teamRanking;
	public List<Players> Players;
}


[System.Serializable]
public class TeamList
{
	public List<TeamInfo> Teams;
}
