using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class feilder_working : MonoBehaviour {

	Animator keeperAnim;
	Animator ballAnim;
	int touch;
	int updated;
	int keepercaught;
	public FastBall fs;
	public Canvas mainmenu;
	static int canvasRendered;
	Vector3 curVel;
	Vector3 curPos;
	static int temp;
	public GameObject mainObj;
	public globalMatchManager gm;




	void Start () {
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		string jsonData = File.ReadAllText(@"Assets/jsonSettings.txt");
		jsonConfig json = JsonUtility.FromJson<jsonConfig>(jsonData);


		keeperAnim = GameObject.Find ("keeper").GetComponent<Animator> ();
		ballAnim = GameObject.Find ("bowler").GetComponent<Animator> ();
		touch = 0;
		if(GameObject.Find ("Main Camera"))
			fs = GameObject.Find ("Main Camera").GetComponent<FastBall> ();
		else
			fs = GameObject.Find ("ground").GetComponent<FastBall> ();
		updated = 0;
		mainmenu.gameObject.SetActive(false);
		canvasRendered = 0;
		keepercaught = 0;
		temp = 0;
	}

	IEnumerator DelayThrow(){
		keeperAnim.SetBool ("throw", true);
		keeperAnim.Play("throw", 0);
		yield return new WaitForSeconds(5);
	}

	IEnumerator Reset(){
		yield return new WaitForSeconds(5);		
		Application.LoadLevel("cricket");
	}
	void Update () {
		if (fs == null)
			fs = GameObject.Find("Bat1").GetComponent<FastBall>();
		if ((GameObject.Find ("Sphere") && (canvasRendered == 0))) {
			curVel = GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity;
			curPos=GameObject.Find ("Sphere").transform.position;
		}
		Keeper_code ();
		Infeilder_code ("feilder");
		Infeilder_code ("feilder1");
		Outfeilder_code ("feilder2");
		Outfeilder_code ("feilder3");
		Outfeilder_code ("feilder4");
		Outfeilder_code ("feilder5");
		Infeilder_code ("feilder6");
		Outfeilder_code ("feilder7");
		Infeilder_code ("feilder8");
		if (GameObject.Find ("Sphere")) {
			if (GameObject.Find ("Sphere").transform.position.y < -1.07) {	
				touch++;
				//				Debug.Log (touch);
			}
		}


		if (gm.isGameOver == true) {
			canvasRendered = 2;
			mainmenu.gameObject.SetActive (true);
			ballAnim.SetBool ("wait", true);
			ballAnim.Play ("wait", 0);
			if (GameObject.Find ("Sphere")) {
				GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
				GameObject.Find ("Sphere").transform.position = curPos;
			}
			if (SteamVR_Controller.Input (SteamVR_Controller.GetDeviceIndex (SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown ()) {
				Application.LoadLevel ("Own_menu");
			}
		}

 		if(SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
		{
			if (canvasRendered == 0) {
				mainmenu.gameObject.SetActive (true);
				canvasRendered = 1;
				ballAnim.SetBool ("wait", true);
				ballAnim.Play ("wait", 0);
				if (GameObject.Find ("Sphere")) {
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
					GameObject.Find ("Sphere").transform.position = curPos;
				}
			} else {
				mainmenu.gameObject.SetActive (false);
				canvasRendered = 0;
				if (GameObject.Find ("bowler").transform.position.z < GameObject.Find ("backline").transform.position.z) {
					ballAnim.SetBool ("wait", false);
					ballAnim.Play ("wait", 0);
				} else {
					ballAnim.SetBool ("idel", true);
					ballAnim.Play ("idel", 0);
				}
				if (GameObject.Find ("Sphere")) {
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = curVel;
					GameObject.Find ("Sphere").transform.position = curPos;
				}
			}
		}

	}

	void Keeper_code()
	{
		if(canvasRendered==0){
		if (GameObject.Find ("Sphere")) {
			Vector3 ball_pos = new Vector3 (GameObject.Find ("Sphere").transform.position.x, GameObject.Find ("Sphere").transform.position.y, GameObject.Find ("Sphere").transform.position.z); 
			Vector3 feilder_pos = new Vector3 (GameObject.Find ("keeper").transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find ("keeper").transform.position.z);
			Vector3 wicket_pos = new Vector3 (GameObject.Find ("frontstump_middle").transform.position.x, GameObject.Find ("frontstump_middle").transform.position.y, GameObject.Find ("frontstump_middle").transform.position.z);
			float ball_wickets=((ball_pos.x - wicket_pos.x) * (ball_pos.x - wicket_pos.x)) + ((ball_pos.y - wicket_pos.y) * (ball_pos.y - wicket_pos.y)) + ((ball_pos.z - wicket_pos.z) * (ball_pos.z - wicket_pos.z));
			float difference = ((ball_pos.x - feilder_pos.x) * (ball_pos.x - feilder_pos.x)) + ((ball_pos.y - feilder_pos.y) * (ball_pos.y - feilder_pos.y)) + ((ball_pos.z - feilder_pos.z) * (ball_pos.z - feilder_pos.z));
			difference=Mathf.Sqrt (difference);
			ball_wickets = Mathf.Sqrt (ball_wickets);
			if (difference < 2)
				GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
			if (difference < 10) {
				if ((feilder_pos.x - ball_pos.x < 5) && (feilder_pos.x - ball_pos.x > 1))
					GameObject.Find ("keeper").transform.position = new Vector3 (GameObject.Find ("keeper").transform.position.x - 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find ("keeper").transform.position.z);
				if ((feilder_pos.x - ball_pos.x > -5) && (feilder_pos.x - ball_pos.x < -1))
					GameObject.Find ("keeper").transform.position = new Vector3 (GameObject.Find ("keeper").transform.position.x + 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find ("keeper").transform.position.z);
				if ((feilder_pos.z - ball_pos.z < 2) && (feilder_pos.z - ball_pos.z > 1))
					GameObject.Find ("keeper").transform.position = new Vector3 (GameObject.Find ("keeper").transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find ("keeper").transform.position.z - .3f);
				if ((feilder_pos.z - ball_pos.z > -2) && (feilder_pos.z - ball_pos.z < -1))
					GameObject.Find ("keeper").transform.position = new Vector3 (GameObject.Find ("keeper").transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find ("keeper").transform.position.z + .3f);
			}
			if ((GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z ==0f)&&(difference < 2)) {
					keepercaught = 1;
				GameObject.Find ("Sphere").transform.position = new Vector3 (GameObject.Find ("Sphere").transform.position.x,GameObject.Find ("Sphere").transform.position.y+1.5f,GameObject.Find ("Sphere").transform.position.z);
				var heading = GameObject.Find ("frontstump_middle").transform.position - GameObject.Find ("Sphere").transform.position;
				StartCoroutine (DelayThrow());
				GameObject.Find ("Sphere").GetComponent<Rigidbody> ().AddForce (heading * 100);
				Debug.Log ("keeper threw the ball");
					if ((touch < 3) && (updated == 0)) {
					if (fs.hit == 0)
						fs.Runs (0, 0);
					else
						fs.Runs (0, 1);
					updated = 1;
				}else
					if(updated==0)
						fs.Runs (0,0);
			}
			if (ball_wickets<1) {
				Debug.Log ("ball at stumps");
				GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
					if ((keepercaught == 0)&&(temp==0)) {
						if (fs == null)
							fs =GameObject.Find("Bat1").GetComponent<FastBall>();
						fs.Runs (0, 1);
						temp = 1;
					}
					StartCoroutine (Reset());
				}
			}
		}
	}


	void Infeilder_code(string feilder)
	{
		if (canvasRendered == 0) {
			Vector3 feilder_pos = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find (feilder).transform.position.y, GameObject.Find (feilder).transform.position.z);
			Vector3 wicket_pos = new Vector3 (GameObject.Find ("frontstump_middle").transform.position.x, GameObject.Find ("frontstump_middle").transform.position.y, GameObject.Find ("frontstump_middle").transform.position.z);
			if (GameObject.Find ("Sphere")) {
				Vector3 ball_pos = new Vector3 (GameObject.Find ("Sphere").transform.position.x, GameObject.Find ("Sphere").transform.position.y, GameObject.Find ("Sphere").transform.position.z); 
				float difference = ((ball_pos.x - feilder_pos.x) * (ball_pos.x - feilder_pos.x)) + ((ball_pos.y - feilder_pos.y) * (ball_pos.y - feilder_pos.y)) + ((ball_pos.z - feilder_pos.z) * (ball_pos.z - feilder_pos.z));
				difference = Mathf.Sqrt (difference);
				if (difference < 2)
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
				if (difference < 25) {
					if ((feilder_pos.x - ball_pos.x < 10) && (feilder_pos.x - ball_pos.x > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x - 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.x - ball_pos.x > -10) && (feilder_pos.x - ball_pos.x < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x + 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y < 10) && (feilder_pos.y - ball_pos.y > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y - 0.3f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y > -10) && (feilder_pos.y - ball_pos.y < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y + 0.3f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.z - ball_pos.z < 10) && (feilder_pos.z - ball_pos.z > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z - .3f);
					if ((feilder_pos.z - ball_pos.z > -10) && (feilder_pos.z - ball_pos.z < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z + .3f);
				}
				if ((GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z == 0f) && (difference < 2)) {
					Debug.Log ("vel 0" + difference);
					Debug.Log ("feilder caught");
					var heading = GameObject.Find ("keeper").transform.position - GameObject.Find ("Sphere").transform.position;
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().AddForce (heading * 100);
					StartCoroutine (DelayThrow ());
					Debug.Log ("feilder threw the ball");
					if ((touch < 3) && (updated == 0)) {
						fs.Runs (0, 1);
						updated = 1;
					} else {
						float ball_wicket = ((feilder_pos.x - wicket_pos.x) * (feilder_pos.x - wicket_pos.x)) + ((feilder_pos.y - wicket_pos.y) * (feilder_pos.y - wicket_pos.y)) + ((feilder_pos.z - wicket_pos.z) * (feilder_pos.z - wicket_pos.z));
						ball_wicket = Mathf.Sqrt (ball_wicket);
						if ((ball_wicket > 150) && (updated == 0)) {
							fs.Runs (4, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 100) && (ball_wicket < 150) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (3, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 30) && (ball_wicket < 70) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (1, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 70) && (ball_wicket < 100) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (2, 0);
							updated = 1;
						}
					}
				}
			}
		}
	}


	void Outfeilder_code(string feilder)
	{
		if (canvasRendered == 0) {
			Vector3 feilder_pos = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find (feilder).transform.position.y, GameObject.Find (feilder).transform.position.z);
			Vector3 wicket_pos = new Vector3 (GameObject.Find ("frontstump_middle").transform.position.x, GameObject.Find ("frontstump_middle").transform.position.y, GameObject.Find ("frontstump_middle").transform.position.z);
			if (GameObject.Find ("Sphere")) {
				Vector3 ball_pos = new Vector3 (GameObject.Find ("Sphere").transform.position.x, GameObject.Find ("Sphere").transform.position.y, GameObject.Find ("Sphere").transform.position.z); 
				float difference = ((ball_pos.x - feilder_pos.x) * (ball_pos.x - feilder_pos.x)) + ((ball_pos.y - feilder_pos.y) * (ball_pos.y - feilder_pos.y)) + ((ball_pos.z - feilder_pos.z) * (ball_pos.z - feilder_pos.z));
				difference = Mathf.Sqrt (difference);
				if (difference < 2)
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
				if (difference < 40) {
					if ((feilder_pos.x - ball_pos.x < 15) && (feilder_pos.x - ball_pos.x > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x - 0.6f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.x - ball_pos.x > -15) && (feilder_pos.x - ball_pos.x < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x + 0.6f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y < 15) && (feilder_pos.y - ball_pos.y > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y - 0.6f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y > -15) && (feilder_pos.y - ball_pos.y < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y + 0.6f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.z - ball_pos.z < 15) && (feilder_pos.z - ball_pos.z > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z - .6f);
					if ((feilder_pos.z - ball_pos.z > -15) && (feilder_pos.z - ball_pos.z < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z + .6f);
				}
				if ((GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z == 0f) && (difference < 2)) {
					Debug.Log ("vel 0" + difference);
					Debug.Log ("feilder caught");
					var heading = GameObject.Find ("keeper").transform.position - GameObject.Find ("Sphere").transform.position;
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().AddForce (heading * 100);
					StartCoroutine (DelayThrow ());
					Debug.Log ("feilder threw the ball");
					if ((touch < 3) && (updated == 0)) {
						fs.Runs (0, 1);
						updated = 1;
					} else {
						float ball_wicket = ((feilder_pos.x - wicket_pos.x) * (feilder_pos.x - wicket_pos.x)) + ((feilder_pos.y - wicket_pos.y) * (feilder_pos.y - wicket_pos.y)) + ((feilder_pos.z - wicket_pos.z) * (feilder_pos.z - wicket_pos.z));
						ball_wicket = Mathf.Sqrt (ball_wicket);
						if ((ball_wicket > 150) && (updated == 0)) {
							fs.Runs (4, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 100) && (ball_wicket < 150) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (3, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 30) && (ball_wicket < 70) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (1, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 70) && (ball_wicket < 100) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (2, 0);
							updated = 1;
						}
					}
				}
			}
		}
	}



	public void bowler_code(string feilder)
	{
		if (canvasRendered == 0) {
			Vector3 feilder_pos = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find (feilder).transform.position.y, GameObject.Find (feilder).transform.position.z);
			Vector3 wicket_pos = new Vector3 (GameObject.Find ("frontstump_middle").transform.position.x, GameObject.Find ("frontstump_middle").transform.position.y, GameObject.Find ("frontstump_middle").transform.position.z);
			if (GameObject.Find ("Sphere")) {
				Vector3 ball_pos = new Vector3 (GameObject.Find ("Sphere").transform.position.x, GameObject.Find ("Sphere").transform.position.y, GameObject.Find ("Sphere").transform.position.z); 
				float difference = ((ball_pos.x - feilder_pos.x) * (ball_pos.x - feilder_pos.x)) + ((ball_pos.y - feilder_pos.y) * (ball_pos.y - feilder_pos.y)) + ((ball_pos.z - feilder_pos.z) * (ball_pos.z - feilder_pos.z));
				difference = Mathf.Sqrt (difference);
				if (difference < 2)
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
				if (difference < 5) {
					if ((feilder_pos.x - ball_pos.x < 3) && (feilder_pos.x - ball_pos.x > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x - 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.x - ball_pos.x > -3) && (feilder_pos.x - ball_pos.x < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x + 0.3f, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y < 3) && (feilder_pos.y - ball_pos.y > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y - 0.3f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.y - ball_pos.y > -3) && (feilder_pos.y - ball_pos.y < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y + 0.3f, GameObject.Find (feilder).transform.position.z);
					if ((feilder_pos.z - ball_pos.z < 3) && (feilder_pos.z - ball_pos.z > 1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z - .3f);
					if ((feilder_pos.z - ball_pos.z > -3) && (feilder_pos.z - ball_pos.z < -1))
						GameObject.Find (feilder).transform.position = new Vector3 (GameObject.Find (feilder).transform.position.x, GameObject.Find ("keeper").transform.position.y, GameObject.Find (feilder).transform.position.z + .3f);
				}
				if ((GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z == 0f) && (difference < 2)) {
					Debug.Log ("vel 0" + difference);
					Debug.Log ("feilder caught");
					var heading = GameObject.Find ("keeper").transform.position - GameObject.Find ("Sphere").transform.position;
					GameObject.Find ("Sphere").GetComponent<Rigidbody> ().AddForce (heading * 100);
					StartCoroutine (DelayThrow ());
					Debug.Log ("feilder threw the ball");
					if ((touch < 3) && (updated == 0)) {
						fs.Runs (0, 1);
						updated = 1;
					} else {
						float ball_wicket = ((feilder_pos.x - wicket_pos.x) * (feilder_pos.x - wicket_pos.x)) + ((feilder_pos.y - wicket_pos.y) * (feilder_pos.y - wicket_pos.y)) + ((feilder_pos.z - wicket_pos.z) * (feilder_pos.z - wicket_pos.z));
						ball_wicket = Mathf.Sqrt (ball_wicket);
						if ((ball_wicket > 150) && (updated == 0)) {
							fs.Runs (4, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 100) && (ball_wicket < 150) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (3, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 30) && (ball_wicket < 70) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (1, 0);
							updated = 1;
						} else if ((updated == 0) && (ball_wicket > 70) && (ball_wicket < 100) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.x < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.z < 5) && (GameObject.Find ("Sphere").GetComponent<Rigidbody> ().velocity.y < 5)) {
							fs.Runs (2, 0);
							updated = 1;
						}
					}
				}
			}
		}
	}
}
