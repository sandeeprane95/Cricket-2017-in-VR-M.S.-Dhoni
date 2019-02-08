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

	public static List<YourPlayer> yourTeamPlayers;
	public static List<YourPlayer> opponentTeamPlayers;

	public static string jsonData = File.ReadAllText(@"Assets/crickData.json");
	public TeamList teams = JsonUtility.FromJson<TeamList>(jsonData);

	public static string jsonData1 = File.ReadAllText(@"Assets/jsonSettings.txt");
	public jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData1);

	public int ballsDelivered = 0;
	public int totalBalls = 30;

	public GameObject mainObj;
	public globalMatchManager matchMngr;
	public scoreBoardManager sbm;

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

	public void updateScatterPlot()
	{
		for (int i = 0; i < 4; i++) {
			string objConst;
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			obj.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, (float)allOvers[i]/(float)10);
			objConst = "over" + (i+1).ToString() + "Score";
			obj = GameObject.Find (objConst);							
			obj.GetComponent<Text>().text = allOvers[i].ToString()+" RUNS";

		}
	}

	public void updateBarGraph()
	{
		for (int i = 0; i < 5; i++) {
			int init = -170;
			float returned;
			string objConst;
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2(obj.GetComponent<RectTransform> ().anchoredPosition.x, (float)(init + allOvers [i] * 8));
			objConst = "over" + (i+1).ToString() + "Score1";
			obj = GameObject.Find (objConst);							
			obj.GetComponent<Text>().text = allOvers[i].ToString()+" RUNS";

		}
	}
	public void updateTriggerCanvas(){
		
		GameObject triggerCanvas = GameObject.Find ("Canvas");
		GameObject scoreBoardPanel;
		string na = "--";
		for (int i = 0; i < 11; i++) {
			if (i == 0) {
				scoreBoardPanel = GameObject.Find ("Names");
				scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].playerName.ToString();
				scoreBoardPanel = GameObject.Find ("Runs");
				scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].score.ToString();
				scoreBoardPanel = GameObject.Find ("Balls");
				scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].balls.ToString();
				scoreBoardPanel = GameObject.Find ("Strike Rate");
				scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].stRate.ToString();
			} else {
				if (i < fallOfWickets + 2) {
					scoreBoardPanel = GameObject.Find ("Names (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].playerName;
					scoreBoardPanel = GameObject.Find ("Runs (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].score.ToString ();
					scoreBoardPanel = GameObject.Find ("Balls (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].balls.ToString ();
					scoreBoardPanel = GameObject.Find ("Strike Rate (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].stRate.ToString ();
				} else {
					scoreBoardPanel = GameObject.Find ("Names (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].playerName;
					scoreBoardPanel = GameObject.Find ("Runs (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = na;
					scoreBoardPanel = GameObject.Find ("Balls (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = na;
					scoreBoardPanel = GameObject.Find ("Strike Rate (" + (i - 1).ToString () + ")");
					scoreBoardPanel.GetComponent<Text> ().text = na;
				}
			}					
		}
		updateBarGraph ();
	}


	public void updateCurrentBatsmentMeta()
	{

		YourPlayer player;
		for (int i = 0; i < yourTeamPlayers.Count; i++) {
			if (yourTeamPlayers [i].playerName == currentbatsmen) {

				int diff = score - prevScore;
				prevScore = score;


				yourTeamPlayers [i].score = yourTeamPlayers [i].score + diff; //TODO: Original Score
				yourTeamPlayers [i].balls++;
				yourTeamPlayers [i].stRate = ((float)yourTeamPlayers [i].score / (float)yourTeamPlayers [i].balls) * (float)100;

				if (diff % 2 == 1) {

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
		Debug.Log ("GAME FINISHED!!!!");
	}

	// Update is called once per frame
	void Update () {


		if (!isGameOver){
		if (prevBallNum != ballCount) {
				updateTriggerCanvas ();
			//TODO: orignal score
				if (ballCount % 6 == 0) {
					if (ballCount / 6 == 1)
						allOvers[(ballCount / 6) - 1] = score;
					else{
						int prevOver = (ballCount / 6) - 2;
						int prevOverRuns = allOvers[prevOver];
						allOvers[(ballCount / 6) - 1] = score;
					}
				}

			prevBallNum = ballCount;

			//Should preserve the order
			updateCurrentBatsmentMeta ();
			updateScoreCanvas ();
		}
		if (ballCount == 30 || fallOfWickets == 10 || score >= target) {

				if (ballCount % 6 != 0) {
					int prevOverRuns = allOvers[(ballCount / 6) - 1];
					allOvers.Add (score-prevOverRuns);
				}
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



