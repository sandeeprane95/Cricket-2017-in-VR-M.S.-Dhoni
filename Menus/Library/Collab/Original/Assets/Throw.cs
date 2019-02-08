using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Throw : MonoBehaviour {

	int k;
	public scoreBoardManager sb;
	public GameObject mainObj;
	public globalMatchManager gm;

	void Start () {
		mainObj = GameObject.Find ("dummyxyz");
		gm = mainObj.GetComponent<globalMatchManager> ();
		string jsonData = File.ReadAllText(@"Assets/jsonSettings.txt");
		jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData);
		sb = new scoreBoardManager ();
		string matchText = json.myTeam + " vs " + json.opponentTeam;
		sb.setMatchText (matchText);

		k=0;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("data cube"))
			Debug.Log ("Found the stored values");

		//		Debug.Log (Time.time);
		float m=0;
		Animator userAnim = GameObject.Find ("bowler").GetComponent<Animator> ();
		if (GameObject.Find ("backline").transform.position.z > GameObject.Find ("bowler").transform.position.z) {
			if (k == 0) {
				GameObject ball;
				ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				ball.name = "Sphere";
				ball.AddComponent<Rigidbody>();
				ball.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				ball.transform.position = new Vector3 (GameObject.Find ("bowler").transform.position.x,GameObject.Find ("backline").transform.position.y+3.5f,GameObject.Find ("bowler").transform.position.z);
				ball.GetComponent<Renderer> ().material = Resources.Load ("New Material 1") as Material;
				GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, -2f);
				ball.GetComponent<SphereCollider> ().material = Resources.Load ("New Physic Material") as PhysicMaterial;
				ball.GetComponent<Rigidbody> ().AddForce (-transform.forward * 800);
				k++;
			}
		} 
		if (k == 1) {
			//GameObject.Find ("bowler").GetComponent<Anebugimator>().enabled=false;
			userAnim.SetBool ("jump", true);
			userAnim.Play ("jump", 0);
			k++;
			m = Time.time;
			}
		if (k == 2) {			
			if (Time.time > m + 2.5) {
				userAnim.SetBool ("wait", true);
				userAnim.Play ("wait", 0);
			}
		}
	}
}

[System.Serializable]
public class jsonConfig
{
	public string myTeam;
	public string opponentTeam;
	public string Difficulty;
	public string Overs;
	public string Stadium;
}