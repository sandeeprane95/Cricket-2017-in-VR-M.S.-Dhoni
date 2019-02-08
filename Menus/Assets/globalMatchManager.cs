using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class globalMatchManager : MonoBehaviour {

	// Use this for initialization
	public int score, ballCount, bouncerCount, player1Score, player2Score, target, fallOfWickets, prevBallNum;
	public string player1Name, player2Name, bowlerName,yourTeam,currentbatsmen,oppTeam;
	public float nrr;
	public bool globalOutStatus = false;

	public int prevScore;

	public List<int> eachOver;
	public List<int> allOvers;

	public bool isGameOver = false;
	public bool didWon = false;

	public static bool executedOnce = false;
	public List<int> autoPlayScores;

	public List<YourPlayer> yourTeamPlayers;
	public List<YourPlayer> opponentTeamPlayers;

	public static string jsonData = File.ReadAllText(@"Assets/crickData.json");
	public TeamList teams = JsonUtility.FromJson<TeamList>(jsonData);

	public static string jsonData1 = File.ReadAllText(@"Assets/jsonSettings.txt");
	public jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData1);
	public int runsTillPrevOver;

	public bool newBatsmenFlag = false;
	public int ballsDelivered = 0;
	public int totalBalls;

	public GameObject mainObj;
	public globalMatchManager matchMngr;
	public scoreBoardManager sbm;
	public triggerCanvasManager tcm;

	public ScoreGenerator sgen;

	private static globalMatchManager _instance;

	public static globalMatchManager Instance 
	{ 
		get { return _instance; } 
	} 

	private void Awake() 
	{ 
		if (_instance != null && _instance != this) 
		{ 
			Destroy(this.gameObject); 
		} 
		else 
		{ 
			_instance = this;

			DontDestroyOnLoad(this.gameObject);
		} 
	} 


	void Start () {

		mainObj = GameObject.Find ("undestructable");
		matchMngr = mainObj.GetComponent<globalMatchManager> ();


		score = 0;
		ballCount = 0;
		bouncerCount = 0;
		player1Score = 0;
		player2Score = 0;
		target = 0;
		fallOfWickets = 0;
		prevBallNum = 0;

		//matchMngr = mainObj.GetComponent<globalMatchManager>();
		sbm = new scoreBoardManager ();

	}



	public void init()
	{

		if (!executedOnce) {

			json = JsonUtility.FromJson<jsonConfig>(jsonData1);
			teams = JsonUtility.FromJson<TeamList>(jsonData);

			yourTeamPlayers = new List<YourPlayer> (11);
			opponentTeamPlayers = new List<YourPlayer> (11);
			allOvers = new List<int> (5);
			for (int i = 0; i < 5; i++)
				allOvers.Add (0);

			executedOnce = true;
			sgen = new ScoreGenerator ();
			target = sgen.setTarget (json.myTeam, json.opponentTeam);
			autoPlayScores = sgen.populateAutoPlayScore (target);
			int autoPlayBatsmenCount = autoPlayScores.Count;

			yourTeam = json.myTeam;
			oppTeam = json.opponentTeam;

			TeamInfo teamInfo;
			Players player = new Players();

			for (int i = 0; i < teams.Teams.Count; i++) {
				teamInfo = teams.Teams [i];
				if (teamInfo.team == yourTeam) {
					currentbatsmen = player1Name;
					for (int iterj = 0; iterj < teamInfo.Players.Count; iterj++) {						
						YourPlayer yourTeamPlayer = new YourPlayer ();
						yourTeamPlayer.playerName = teamInfo.Players [iterj].name;
						yourTeamPlayer.score = 0;
						yourTeamPlayer.balls = 0;
						yourTeamPlayer.fours = 0;
						yourTeamPlayer.sixes = 0;
						yourTeamPlayer.stRate = 0;
						yourTeamPlayer.fow = 0;

						yourTeamPlayers.Add (yourTeamPlayer);
					}
					player1Name = yourTeamPlayers [0].playerName;
					player2Name = yourTeamPlayers [1].playerName;
					currentbatsmen = player1Name;

				} else if (teamInfo.team == oppTeam) {

					for (int iterj = 0; iterj < teamInfo.Players.Count ; iterj++) {

						YourPlayer oppTeamPlayer = new YourPlayer ();
						oppTeamPlayer.playerName = teamInfo.Players [iterj].name;
						if(iterj<autoPlayBatsmenCount)
							oppTeamPlayer.score = autoPlayScores[iterj];
						else
							oppTeamPlayer.score = 0;
						oppTeamPlayer.balls = 0;
						oppTeamPlayer.fours = 0;
						oppTeamPlayer.sixes = 0;
						oppTeamPlayer.stRate = 0;
						oppTeamPlayer.fow = 0;
						opponentTeamPlayers.Add (oppTeamPlayer);
					}
				}

			}


		}

		updateScoreCanvas ();
		updateTriggerCanvas ();
	}

	// Update is called once per frame

	/*
	void Start () {
		matchMngr = mainObj.GetComponent<globalMatchManager>();
		sbm = mainObj.GetComponent<scoreBoardManager> ();

	}
	*/

	public void updateScoreCanvas()
	{
		sbm = new scoreBoardManager ();

		YourPlayer player;
		//sbm.setBowlerText ("bret lee"); //TODO: Original bowler
		string noOfOvers = (ballCount/6).ToString() + "." + (ballCount%6).ToString();
		if(ballCount == 0)
			sbm.setNRRText ("0");
		else			
			sbm.setNRRText ((score*6/ballCount).ToString());

		bowlerName = opponentTeamPlayers [10 - ballCount / 6].playerName;
		sbm.setBowlerText (bowlerName);

		player = getPlayerDetails (player1Name);
		player1Score = player.score;
		player = getPlayerDetails (player2Name);
		player2Score = player.score;

		if (currentbatsmen == player1Name) {
			sbm.setPlayer1Text (player1Name + "*");
			sbm.setPlayer2Text (player2Name);
		} else {
			sbm.setPlayer1Text (player1Name);
			sbm.setPlayer2Text (player2Name+"*");
		}
		sbm.setScoreText (score.ToString());
		sbm.setOversText (noOfOvers.ToString());
		sbm.setUpdateText ();
	}

	public YourPlayer getPlayerDetails(string name)
	{
		YourPlayer player;
		for (int i = 0; i < yourTeamPlayers.Count; i++) {
			if (yourTeamPlayers [i].playerName == name) {
				player = yourTeamPlayers [i];
				return player;				
			}
		}
		return null;
	}



	public void updateTriggerCanvas(){
		handleEndGame ();
		tcm = new triggerCanvasManager ();
		tcm.updateTriggerCanvas ();
	}


	public void updateCurrentBatsmentMeta()
	{
		sbm = new scoreBoardManager ();
		YourPlayer player;
		for (int i = 0; i < yourTeamPlayers.Count; i++) {
			if (yourTeamPlayers [i].playerName == currentbatsmen) {

				int diff = score - prevScore;
				prevScore = score;


				yourTeamPlayers [i].score = yourTeamPlayers [i].score + diff; //TODO: Original Score
				yourTeamPlayers [i].balls++;
				yourTeamPlayers [i].stRate = ((float)yourTeamPlayers [i].score / (float)yourTeamPlayers [i].balls) * (float)100;
				sbm.currentScoreUpdate (yourTeamPlayers [i].playerName, diff.ToString(), globalOutStatus);
				if (diff % 2 == 1) {

					if (currentbatsmen == player1Name)
						currentbatsmen = player2Name;
					else
						currentbatsmen = player1Name;									
				}
				if (ballCount % 6 == 0) {

					if (currentbatsmen == player1Name)
						currentbatsmen = player2Name;
					else
						currentbatsmen = player1Name;									
				}

				//TODO:Update 4's,6's, and st rate
				if (globalOutStatus) { //TODO: if out or not
					yourTeamPlayers[i].isOut = true;
					yourTeamPlayers [i].fow = ++fallOfWickets;
					if (fallOfWickets < 10) {
						if (yourTeamPlayers [i].playerName == player1Name)
							player1Name = yourTeamPlayers [fallOfWickets + 1].playerName;
						else
							player2Name = yourTeamPlayers [fallOfWickets + 1].playerName;
						currentbatsmen = yourTeamPlayers [fallOfWickets + 1].playerName;
					}
					globalOutStatus = false;

				}
				break;
			}
		}
	}


	public void handleEndGame()
	{
		if (isGameOver) {
			sbm = new scoreBoardManager ();
			Debug.Log ("GAME FINISHED!!!!");
			YourPlayer manOfTheMatch = computeManOfTheMatch ();
			string message;
			if (didWon)
				message = yourTeam + " has Won the Match by" + "\n" + (10 - fallOfWickets).ToString () + " wickets";
			else
				message = oppTeam + " has Won the Match by " + (target-score).ToString () + " runs";
			sbm.updateMatchOverCanvas (message, "Player of the match:" + "\n" + manOfTheMatch.playerName);
		}
	}

	public YourPlayer computeManOfTheMatch(){
		int manOfTheMatchScore = 0;
		int manOfTheMatchIndex = 0;
		if (didWon) {
			for (int i = 0; i < 11; i++) {
				if (yourTeamPlayers [i].score > manOfTheMatchScore) {
					manOfTheMatchScore = yourTeamPlayers [i].score;
					manOfTheMatchIndex = i;
				}
			}
		} else {
			for (int i = 0; i < 11; i++) {
				if (opponentTeamPlayers [i].score > manOfTheMatchScore) {
					manOfTheMatchScore = opponentTeamPlayers [i].score;
					manOfTheMatchIndex = i;
				}
			}
		}
		if (didWon) {
			return yourTeamPlayers [manOfTheMatchIndex];
		} else {
			return opponentTeamPlayers [manOfTheMatchIndex];
		}
	}
	// Update is called once per frame
	void Update () {

		updateTriggerCanvas ();
		if (!isGameOver){
			if (prevBallNum != ballCount) {

				//TODO: orignal score
				if (ballCount % 6 == 0) {
					if (ballCount / 6 == 1) {
						allOvers [(ballCount / 6) - 1] = score;
						runsTillPrevOver = score;
					}
					else{
						allOvers[(ballCount / 6) - 1] = score - runsTillPrevOver;
						runsTillPrevOver = score;
					}
				}

				prevBallNum = ballCount;

				//Should preserve the order
				if (ballCount == totalBalls || fallOfWickets == 10 || score >= target) {
					isGameOver = true;
					if (score >= target)
						didWon = true;
				}
				updateCurrentBatsmentMeta ();
				updateScoreCanvas ();

			}
			if (ballCount == totalBalls || fallOfWickets == 10 || score >= target) {


				isGameOver = true;
				if (score > target)
					didWon = true;
				handleEndGame ();
			}
		}

	}

}



[System.Serializable]
public class YourPlayer
{
	public string playerName;
	public int score;
	public int balls;
	public int fow;
	public float stRate;
	public int fours;
	public int sixes;
	public bool isOut;
}


[System.Serializable]
public class currentTeamPlayers
{
	public List<YourPlayer> players;
}



