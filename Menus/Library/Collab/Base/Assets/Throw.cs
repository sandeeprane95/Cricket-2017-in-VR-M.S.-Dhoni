using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Throw : MonoBehaviour {

	int k,hit;
	public scoreBoardManager sb;
	public GameObject mainObj;
	public globalMatchManager gm;
	Animator userAnim;
	feilder_working fw;
	FastBall fs;

	void Start () {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		string jsonData = File.ReadAllText(@"Assets/jsonSettings.txt");
		jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData);
		sb = new scoreBoardManager ();
		string matchText = json.myTeam + " vs " + json.opponentTeam;
		sb.setMatchText (matchText);
		hit = 0;

		if (json.Overs == "2") {
			gm.totalBalls = 12;
		} else if (json.Overs == "1") {
			gm.totalBalls = 6;
		} else {
			gm.totalBalls = 30;
		}
		gm.init ();
		sb.SetTargetText (gm.target.ToString ());

		setMaterial (json.myTeam);
		k=0;
		userAnim = GameObject.Find ("bowler").GetComponent<Animator> ();
		fw = new feilder_working ();
		fs = new FastBall ();
	}
	IEnumerator DelayThrow(){
		userAnim.SetBool ("jump", true);
		userAnim.Play ("jump", 0);
		yield return new WaitForSeconds(1);
	}

	IEnumerator Delaywait()
	{
		yield return new WaitForSeconds(2);
		userAnim.SetBool ("wait", true);
		userAnim.Play ("wait", 0);
	}
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("data cube"))
			Debug.Log ("Found the stored values");

		//		Debug.Log (Time.time);
		float m=0;

		if (GameObject.Find ("backline").transform.position.z+1 > GameObject.Find ("bowler").transform.position.z) {
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
				float x = Random.Range(0,3);
				float z = Random.Range(-12,-20);
				Vector3 target = new Vector3 (x,-2.427f,z);
				var heading = target - ball.transform.position;
				ball.GetComponent<Rigidbody> ().AddForce (heading * 30);
				k++;
			}
		} 
		if (k == 1) {
			//GameObject.Find ("bowler").GetComponent<Anebugimator>().enabled=false;
			StartCoroutine(DelayThrow());
			k++;
			}
		if (k == 2) {
			StartCoroutine (Delaywait ());
			if (GameObject.Find ("Sphere"))
				if (GameObject.Find ("Sphere").transform.position.z < -3f)
					hit = 1;
			if (hit == 1)
				fw.bowler_code ("bowler");
		}
	}
	void setMaterial(string team)
	{
		
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