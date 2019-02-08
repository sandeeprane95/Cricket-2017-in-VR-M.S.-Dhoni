#pragma strict
import UnityEngine.UI;
import UnityEngine;
import UnityEngine.SceneManagement;
import System.Collections;

var CameraObject : Camera;
var Myteam : GameObject;
var Oponent : GameObject;
var SurePanal : GameObject;
var DifficultyPanal:GameObject;
var OversPanal:GameObject;
var SameTeamPanal:GameObject;
var go:EventSystems.EventSystem;
var myteam;
var oponent;


function Start () {
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	SurePanal.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
	SameTeamPanal.gameObject.active=false;
}

function Update () {
	Debug.Log(myteam+"  "+oponent);
}

function RotateCameraToCanvas2(){
	CameraObject.transform.rotation.y=1;
}

function RotateCameraToCanvas1(){
	CameraObject.transform.rotation.y=0;
}


function MyTeamSelection(){
	Myteam.gameObject.active=true;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
}

function OponentSelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=true;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
}

function AreYouSure(){
	SurePanal.gameObject.active=true;
}


function MyTeam(){
	
}

function DifficultySelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=true;
	OversPanal.gameObject.active=false;
}


function OversSelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=true;
}

function ButtonName(){
	if(go.current=='My Team')
		Debug.Log(go.current.GetType());
}


function SameTeam(){
	SameTeamPanal.gameObject.active=true;
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
}




function OponentIndia(){
	oponent="India";
			if(myteam==oponent)
			SameTeam();
}
function OponentEngland(){

	oponent="England";
		if(myteam==oponent)
			SameTeam();
}
function OponentSriLanka(){
	oponent="Sri Lanka";
		if(myteam==oponent)
			SameTeam();
			}
function OponentPakisthan(){
	oponent="Pakisthan";
			if(myteam==oponent)
			SameTeam();
}
function OponentSouthAfrica(){
	oponent="South Africa";
		if(myteam==oponent)
			SameTeam();
}
function OponentNewZealand(){
	oponent="New Zealand";
		if(myteam==oponent)
			SameTeam();
}
function OponentAustralia(){
	oponent="Australia";
		if(myteam==oponent)
			SameTeam();
}
function OponentUnitedStates(){
	oponent="united states";
		if(myteam==oponent)
			SameTeam();
}


function MyteamIndia(){
	myteam="India";
}
function MyteamEngland(){
	myteam="England";
}
function MyteamSriLanka(){
	myteam="Sri Lanka";
}
function MyteamPakisthan(){
	myteam="Pakisthan";
}
function MyteamSouthAfrica(){
	myteam="South Africa";
}
function MyteamNewZealand(){
	myteam="New Zealand";
}
function MyteamAustralia(){
	myteam="Australia";
}
function MyteamUnitedStates(){
	myteam="united states";
}


function yesexit(){
	Application.Quit();
}

function noexit(){
	SurePanal.gameObject.active=false;	
}



function Back(){
	SameTeamPanal.gameObject.active=false;
}


function Play(){
	Application.LoadLevel("cricket");
} 