using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

	// Use this for initialization
	public Vector3 positionA;
	public List<Vector3> positions;
	public GameObject mainObj;
	public globalMatchManager gm;
	void Start () {
		

	}

	void OnDrawGizmos ()
	{
		
		/*
		for (int i = 0; i < 5; i++) {

			objConst = "over" + (i).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj1 = GameObject.Find (objConst);
			if (((i + 1) * 6) <= gm.ballCount) {
				Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
				Gizmos.DrawLine(positions[i], positions[i+1]);
			}						
		}
		*/
	}


	// Update is called once per frame
	void Update () {

		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();

		GameObject line = GameObject.Find ("scatterPlotLine");
		LineRenderer lr = line.GetComponent<LineRenderer> ();


		string objConst;
		lr.numPositions = (gm.ballCount / 6)+1;
		for (int i = 0; i <= gm.ballCount/6; i++){
			
				objConst = "over" + (i).ToString () + "Mark";
				GameObject obj = GameObject.Find (objConst);
				positionA = obj.transform.position;
				lr.SetPosition (i, positionA);

			}
		}

}
