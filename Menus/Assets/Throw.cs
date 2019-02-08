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

		setMaterial (json.opponentTeam,json.myTeam);
		k=0;
		userAnim = GameObject.Find ("bowler").GetComponent<Animator> ();
		fw = GameObject.Find ("ground").GetComponent<feilder_working> ();
		if(GameObject.Find ("Main Camera"))
			fs = GameObject.Find ("Main Camera").GetComponent<FastBall> ();
		else
			fs = GameObject.Find ("ground").GetComponent<FastBall> ();
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
	void setMaterial(string team,string myteam)
	{
		if (team.Equals ("India")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
		}
		if (team.Equals ("Australia")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material") as Material;
		}
		if (team.Equals ("England")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material") as Material;
		}
		if (team.Equals ("Pakistan")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material") as Material;
		}
		if (team.Equals ("New Zealand")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material") as Material;
		}
		if (team.Equals ("South Africa")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material") as Material;
		}
		if (team.Equals ("United States")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material") as Material;
		}
		if (team.Equals ("Sri Lanka")) {
			GameObject.Find ("bowler/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder3/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder4/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;GameObject.Find ("keeper/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("feilder5/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder6/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder7/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
			GameObject.Find ("feilder8/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material") as Material;
		}

		if (myteam.Equals ("India")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material") as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("india_material")as Material;
		}
		if (myteam.Equals ("Pakistan")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("pakistan_material")as Material;
		}
		if (myteam.Equals ("Australia")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("australia_material")as Material;
		}
		if (myteam.Equals ("England")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("england_material")as Material;
		}
		if (myteam.Equals ("Sri Lanka")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("srilanka_material")as Material;
		}
		if (myteam.Equals ("United States")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("usa_material")as Material;
		}
		if (myteam.Equals ("South Africa")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("southafrica_material")as Material;
		}
		if (myteam.Equals ("New Zealand")) {
			GameObject.Find ("cheerleader/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material")as Material;
			GameObject.Find ("cheerleader1/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material")as Material;
			GameObject.Find ("cheerleader2/dress").GetComponent<SkinnedMeshRenderer> ().material = Resources.Load ("newzealand_material")as Material;
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