using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggerCanvasManager : MonoBehaviour {

	// Use this for initialization

	public List<int> allOvers;
	public List<YourPlayer> yourTeamPlayers;
	public int fallOfWickets;
	public GameObject mainObj;
	public globalMatchManager gm;
	void Start () {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> (); 
	}

	public void updateBarGraph()
	{
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> (); 
		allOvers = gm.allOvers;		
		for (int i = 0; i < 4; i++) {
			string objConst;
			objConst = "over" + (i+1).ToString() + "Bar";
			GameObject obj = GameObject.Find (objConst);
			obj.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, (float)allOvers[i]/(float)10);
			objConst = "over" + (i+1).ToString() + "Score";
			obj = GameObject.Find (objConst);							
			obj.GetComponent<Text>().text = allOvers[i].ToString()+" RUNS";

		}
	}

	public void updateScatterGraph()
	{
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> (); 
		allOvers = gm.allOvers;		
		for (int i = 0; i < 5; i++) {
			int init = -170;
			string objConst;
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			int current = init + (allOvers [i] * 8);
			obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (obj.GetComponent<RectTransform> ().anchoredPosition.x,current);
			objConst = "over" + (i+1).ToString() + "Score1";
			obj = GameObject.Find (objConst);							
			obj.GetComponent<Text>().text = allOvers[i].ToString()+" RUNS";
		}
	}

	public void updateTriggerCanvas(){
		
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> (); 

		yourTeamPlayers = gm.yourTeamPlayers;
		fallOfWickets = gm.fallOfWickets;

		if (GameObject.Find ("details")) {
			GameObject triggerCanvas = GameObject.Find ("Canvas");
			GameObject scoreBoardPanel;
			if (GameObject.Find ("details")) {
				string na = "--";
				for (int i = 0; i < 11; i++) {
					if (i == 0) {
						scoreBoardPanel = GameObject.Find ("Names");
						scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].playerName.ToString ();
						scoreBoardPanel = GameObject.Find ("Runs");
						scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].score.ToString ();
						scoreBoardPanel = GameObject.Find ("Balls");
						scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].balls.ToString ();
						scoreBoardPanel = GameObject.Find ("Strike Rate");
						scoreBoardPanel.GetComponent<Text> ().text = yourTeamPlayers [i].stRate.ToString ();
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
				updateScatterGraph ();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
