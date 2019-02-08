﻿using System.Collections;
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
		mainObj = GameObject.Find ("undestructable");
		gm = mainObj.GetComponent<globalMatchManager> ();
		positions = new List<Vector3> ();
		string objConst;
		for (int i = 0; i < 5; i++) {
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			positionA = obj.transform.position;
			positions.Add (positionA);
		}

	}

	void OnDrawGizmos ()
	{
		string objConst;
		for (int i = 0; i < 5; i++) {
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			positionA = obj.transform.position;
			positions [i] = positionA;
		}
		for (int i = 0; i < 4; i++) {

			objConst = "over" + (i).ToString() + "Mark";
			GameObject obj = GameObject.Find (objConst);
			objConst = "over" + (i+1).ToString() + "Mark";
			GameObject obj1 = GameObject.Find (objConst);
			if (((i + 1) * 6) <= gm.ballCount) {
				Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
				Gizmos.DrawLine(positions[i], positions[i+1]);
			}						
		}
	}


	// Update is called once per frame
	void Update () {


	}
}
