using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBall : MonoBehaviour {

	int k;
	public int hit;
	int unfeilded;
	public AudioSource fireWorksSource;
	public static int runscalled;
	public AudioSource sound;
	public AudioClip clip;
	public AudioClip fourRunsClip1;
	public AudioClip fourRunsClip2;
	public AudioClip oneRunsClip1;
	public AudioClip oneRunsClip2;
	public AudioClip twoRunsClip1;
	public AudioClip twoRunsClip2;
	public AudioClip threeRunsClip1;
	public AudioClip threeRunsClip2;
	public AudioClip wicketClip;
	public GameObject mainObj;
	globalMatchManager gm;
	Animator cheergirl;


	public GameObject fireWorksRight;
	public GameObject fireWorksLeft;
	public AudioClip fireWorksAudio;

	void Start () {
		k = 0;
		hit = 0;
		unfeilded = 1;
		mainObj = GameObject.Find("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		sound = GameObject.Find ("Pitch").GetComponent<AudioSource> ();
		cheergirl = GameObject.Find ("cheerleader").GetComponent<Animator> ();
		cheergirl.SetBool ("idle", true);
		cheergirl.Play ("idle", 0);
		runscalled = 0;
	}

	IEnumerator DelaySound(){
		sound.clip = clip;
		sound.Play ();
		yield return new WaitForSeconds (5);
		Application.LoadLevel ("cricket");
	}

	IEnumerator DanceDelay(){
		cheergirl.SetBool ("dancing2", true);
		cheergirl.Play ("dancing2", 0);
		yield return new WaitForSeconds (5);
	}

	IEnumerator Dance1Delay(){
		yield return new WaitForSeconds (5);
		cheergirl.SetBool ("dancing1", true);
		cheergirl.Play ("dancing1", 0);
	}
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Sphere"))
        {
            Vector3 ball_pos = new Vector3(GameObject.Find("Sphere").transform.position.x, GameObject.Find("Sphere").transform.position.y, GameObject.Find("Sphere").transform.position.z);
            Vector3 wicket_pos = new Vector3(GameObject.Find("frontstump_middle").transform.position.x, GameObject.Find("frontstump_middle").transform.position.y, GameObject.Find("frontstump_middle").transform.position.z);
            float ball_wickets = ((ball_pos.x - wicket_pos.x) * (ball_pos.x - wicket_pos.x)) + ((ball_pos.y - wicket_pos.y) * (ball_pos.y - wicket_pos.y)) + ((ball_pos.z - wicket_pos.z) * (ball_pos.z - wicket_pos.z));
            ball_wickets = Mathf.Sqrt(ball_wickets);
            Debug.Log("dist   " + ball_wickets);
            float x = GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x;
            float y = GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x;
            float z = GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x;
            Debug.Log("x   " + x + "y   " + y + "z  " + z);
            if (x < 0)
                x = x * -1;
            if (y < 0)
                y = y * -1;
            if (z < 0)
                z = z * -1;
			if (ball_wickets > 180)
				UnfeildedRuns(4,0);
			if (k == 1)
            {
                if (ball_wickets > 180)
					UnfeildedRuns(4,0);
                else if ((ball_wickets > 100) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.z < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.y < 5))
					UnfeildedRuns(3,0);
                else if ((ball_wickets > 30) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.z < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.y < 5))
					UnfeildedRuns(1,0);
                else if ((ball_wickets > 70) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.x < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.z < 5) && (GameObject.Find("Sphere").GetComponent<Rigidbody>().velocity.y < 5))
					UnfeildedRuns(2,0);
            }
        }
	}

	public void OnCollisionEnter(Collision other) {
		int magnitude = 500;
		var force = GameObject.Find("Sphere").transform.position - GameObject.Find("Bat1").transform.position;
		force.Normalize();
		GameObject.Find("Sphere").GetComponent<Rigidbody>().AddForce(force * magnitude);
		k = 1;
		hit = 1;

	}

	public void Runs(int n,int wicket)
	{	
		if (runscalled == 0) {
			runscalled = 1;
			unfeilded = 0;
			if (!gm) {
				mainObj = GameObject.Find ("undestructable");
				gm = mainObj.GetComponent<globalMatchManager> ();
			}
			if (wicket == 1) {
				gm.globalOutStatus = true;
			}
			if (!gm.isGameOver) {
				gm.score = gm.score + n;
				gm.ballCount++;
			}
			if (n >= 4) {
				StartCoroutine (DanceDelay ());
				StartCoroutine (Dance1Delay ());
				Instantiate (fireWorksLeft);
				Instantiate (fireWorksRight);
                //TODO: Audio
				//fireWorksSource.Play();
                StartCoroutine(DelaySound());
            }
			int k = Random.Range (0, 1);
			if ((n == 4) && (k == 0)) {
				clip = fourRunsClip1;
				StartCoroutine (DelaySound ());
			}
			if ((n == 4) && (k == 1)) {
				clip = fourRunsClip2;
				StartCoroutine (DelaySound ());
			}
			if ((n == 1) && (k == 0)) {
				clip = oneRunsClip1;
				StartCoroutine (DelaySound ());
			}
			if ((n == 1) && (k == 1)) {
				clip = oneRunsClip2;
				StartCoroutine (DelaySound ());
			}
			if ((n == 2) && (k == 0)) {
				clip = twoRunsClip1;
				StartCoroutine (DelaySound ());
			}
			if ((n == 2) && (k == 1)) {
				clip = twoRunsClip2;
				StartCoroutine (DelaySound ());
			}
			if ((n == 3) && (k == 0)) {
				clip = threeRunsClip1;
				StartCoroutine (DelaySound ());
			}
			if ((n == 3) && (k == 1)) {
				clip = threeRunsClip2;
				StartCoroutine (DelaySound ());
			}
			if (wicket == 1) {
				clip = wicketClip;
				StartCoroutine (DelaySound ());
			}
		}
	}
	public void UnfeildedRuns(int n,int wicket)
	{
		if (runscalled == 0) {
			runscalled = 1;
			if (unfeilded == 1) {
				if (!gm) {
					mainObj = GameObject.Find ("undestructable");
					gm = mainObj.GetComponent<globalMatchManager> ();
				}
				if (wicket == 1) {
					gm.globalOutStatus = true;
				}


				if (!gm.isGameOver) {
					gm.score = gm.score + n;
					gm.ballCount++;
				}

				if (n >= 4) {
                    StartCoroutine(DanceDelay());
                    StartCoroutine(Dance1Delay());
                    Instantiate (fireWorksLeft);
					Instantiate (fireWorksRight);
                    //TODO: Audio
					//fireWorksSource.Play();
                    StartCoroutine(DelaySound());
                }


				int k = Random.Range (0, 1);
				if ((n == 4) && (k == 0)) {
					clip = fourRunsClip1;
					StartCoroutine (DelaySound ());
				}
				if ((n == 4) && (k == 1)) {
					clip = fourRunsClip2;
					StartCoroutine (DelaySound ());
				}
				if ((n == 1) && (k == 0)) {
					clip = oneRunsClip1;
					StartCoroutine (DelaySound ());
				}
				if ((n == 1) && (k == 1)) {
					clip = oneRunsClip2;
					StartCoroutine (DelaySound ());
				}
				if ((n == 2) && (k == 0)) {
					clip = twoRunsClip1;
					StartCoroutine (DelaySound ());
				}
				if ((n == 2) && (k == 1)) {
					clip = twoRunsClip2;
					StartCoroutine (DelaySound ());
				}
				if ((n == 3) && (k == 0)) {
					clip = threeRunsClip1;
					StartCoroutine (DelaySound ());
				}
				if ((n == 3) && (k == 1)) {
					clip = threeRunsClip2;
					StartCoroutine (DelaySound ());
				}
				if (wicket == 1) {
					clip = wicketClip;
					StartCoroutine (DelaySound ());
				}
			}
		}
	}
}