using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoreBoardManager : MonoBehaviour {

	// Use this for initialization

	public Text scoreText, oversText, nrrText, matchText, updateText, player1Text, player2Text, bowlerText, targetText,currentScoreText,matchWonText,manOfTheMatchText;
	GameObject mainObj;
	globalMatchManager gm;

	void Start () {

		scoreText = GameObject.Find ("scorePanel").GetComponent<Text> ();
		oversText = GameObject.Find ("oversPanel").GetComponent<Text> ();
		nrrText = GameObject.Find ("runRatePanel").GetComponent<Text> ();
		matchText = GameObject.Find ("matchPanel").GetComponent<Text> ();
		player1Text = GameObject.Find ("player1Panel").GetComponent<Text> ();
		player2Text = GameObject.Find ("player2Panel").GetComponent<Text> ();
		bowlerText = GameObject.Find ("bowlerPanel").GetComponent<Text> ();
		targetText = GameObject.Find ("targetPanel").GetComponent<Text> ();
		updateText = GameObject.Find ("updatePanelPanel").GetComponent<Text> ();
	}

	public void setScoreText(string value) {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		scoreText = GameObject.Find ("scorePanel").GetComponent<Text> ();
		scoreText.text = "Score: " + value + "/" + gm.fallOfWickets;
	}

	public void setOversText(string value) {
		oversText = GameObject.Find ("oversPanel").GetComponent<Text> ();
		oversText.text = "Overs: " + value;
	}

	public void setNRRText(string value) {
		nrrText = GameObject.Find ("runRatePanel").GetComponent<Text> ();
		nrrText.text = "Run-Rate: " + value;
	}

	public void setPlayer1Text(string value) {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		player1Text = GameObject.Find ("player1Panel").GetComponent<Text> ();
		player1Text.text = value + ":" + gm.player1Score;
	}

	public void setPlayer2Text(string value) {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		player2Text = GameObject.Find ("player2Panel").GetComponent<Text> ();
		player2Text.text = value + ":" + gm.player2Score;
	}

	public void setBowlerText(string value) {
		bowlerText = GameObject.Find ("bowlerPanel").GetComponent<Text> ();
		bowlerText.text = "Bowler: " + value;
	}

	public void SetTargetText(string value) {
		targetText = GameObject.Find ("targetPanel").GetComponent<Text> ();
		targetText.text = "Target: " + value;
	}
	public void setMatchText(string value) {
		matchText = GameObject.Find ("matchPanel").GetComponent<Text> ();
		matchText.text = value;
	}

	public void setUpdateText() {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		updateText = GameObject.Find ("updatePanel").GetComponent<Text> ();
		if (gm.isGameOver) {
			if (gm.didWon) {
				updateText.text = gm.yourTeam + " won the match!!";
			} else {
				updateText.text = gm.oppTeam + " won the match!!";
			}
		}
		else
			updateText.text = gm.yourTeam + "need " + (gm.target - gm.score).ToString() + " runs to win from " + (gm.totalBalls - gm.ballCount).ToString() + " Balls";

	}

	public void updateMatchOverCanvas(string message, string manOfTheMatch){

		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		matchWonText = GameObject.Find ("matchWonText").GetComponent<Text> ();
		manOfTheMatchText = GameObject.Find ("manOfTheMatchText").GetComponent<Text> ();
		if (GameObject.Find ("details")) {
			matchWonText.text = message;
			manOfTheMatchText.text = manOfTheMatch;
		}					
	}

	public void newBatsmenUpdate(YourPlayer batsmen, string runs, bool isOut){
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();

		currentScoreText = GameObject.Find ("currentScoreUpdate").GetComponent<Text> ();
			
		currentScoreText.text = "New Bastsmen In!!"+"\n"+ "Name:" + batsmen.playerName +" is out!!";


	}

	public void currentScoreUpdate(string batsmenName, string runs, bool isOut){
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		currentScoreText = GameObject.Find ("currentScoreUpdate").GetComponent<Text> ();
		if(isOut)			
			currentScoreText.text = "\n"+batsmenName +" is out!!";
		else{
			currentScoreText.text = "\n"+batsmenName +" has hit " +"\n"+ runs +  " run(s)";
		}

	}
	// Update is called once per frame
	void Update () {

	}
}
