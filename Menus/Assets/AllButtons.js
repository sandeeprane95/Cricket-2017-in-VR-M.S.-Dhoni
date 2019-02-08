#pragma strict
import UnityEngine.UI;
import UnityEngine;
import UnityEngine.SceneManagement;
import System.Collections;
import System.IO;

var CameraObject : Camera;
var Myteam : GameObject;
var Oponent : GameObject;
var SurePanal : GameObject;
var DifficultyPanal:GameObject;
var OversPanal:GameObject;
var SameTeamPanal:GameObject;
var StadiumPanal:GameObject;
var MainCanvas:GameObject;
var SettingsCanvas:GameObject;
var C:GameObject;

var go:EventSystems.EventSystem;
var myteam;
var oponent;
public static var difficulty;
public static var overs;
public static var stadium;


var SoundPanel:GameObject;
var IndiaPanel:GameObject;
var IndiaPanel1:GameObject;
var EnglandPanel:GameObject;
var EnglandPanel1:GameObject;
var AustraliaPanel:GameObject;
var AustraliaPanel1:GameObject;
var SouthAfricaPanel:GameObject;
var SouthAfricaPanel1:GameObject;
var SriLankaPanel:GameObject;
var SriLankaPanel1:GameObject;
var UnitedStatesPanel:GameObject;
var UnitedStatesPanel1:GameObject;
var PakistanPanel:GameObject;
var PakistanPanel1:GameObject;
var NewZealandPanel:GameObject;
var NewZealandPanel1:GameObject;
var graph1:GameObject;
var graph2:GameObject;
var teamdetails:GameObject;



function Start () {
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	SurePanal.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
	SameTeamPanal.gameObject.active=false;
	StadiumPanal.gameObject.active=false;
	MainCanvas.gameObject.active=true;
	SettingsCanvas.gameObject.active=false;
}

function Update () {
	Debug.Log(myteam+"  "+oponent);
}

function RotateCameraToCanvas2(){
Debug.Log("caller called");
	MainCanvas.gameObject.active=false;
	SettingsCanvas.gameObject.active=true;
}

function RotateCameraToCanvas1(){
	MainCanvas.gameObject.active=true;
	SettingsCanvas.gameObject.active=false;
}


function MyTeamSelection(){
	Myteam.gameObject.active=true;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
	StadiumPanal.gameObject.active=false;
	SoundPanel.gameObject.active=false;
}

function SoundSelection(){
    SoundPanel.gameObject.active=true;
    Myteam.gameObject.active=false;
    Oponent.gameObject.active=false;
    DifficultyPanal.gameObject.active=false;
    OversPanal.gameObject.active=false;
    StadiumPanal.gameObject.active=false;
}

function OponentSelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=true;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
	StadiumPanal.gameObject.active=false;
	SoundPanel.gameObject.active=false;
}

function StadiumSelection()
{
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=false;
	StadiumPanal.gameObject.active=true;
	SoundPanel.gameObject.active=false;
}

function AreYouSure(){
	SurePanal.gameObject.active=true;
}


function DifficultySelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=true;
	OversPanal.gameObject.active=false;
	StadiumPanal.gameObject.active=false;
	SoundPanel.gameObject.active=false;
}


function OversSelection(){
	Myteam.gameObject.active=false;
	Oponent.gameObject.active=false;
	DifficultyPanal.gameObject.active=false;
	OversPanal.gameObject.active=true;
	StadiumPanal.gameObject.active=false;
	SoundPanel.gameObject.active=false;
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
	StadiumPanal.gameObject.active=false;
	SoundPanel.gameObject.active=false;
}




function OponentIndia(){
    oponent="India";
    IndiaPanel1.gameObject.active=true;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
    if(myteam==oponent)
			SameTeam();
}
function OponentEngland(){

    oponent="England";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=true;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
		if(myteam==oponent)
			SameTeam();
}
function OponentSriLanka(){
    oponent="Sri Lanka";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=true;
		if(myteam==oponent)
			SameTeam();
			}
function OponentPakisthan(){
    oponent="Pakistan";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=true;
    SriLankaPanel1.gameObject.active=false;
			if(myteam==oponent)
			SameTeam();
}
function OponentSouthAfrica(){
    oponent="South Africa";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=true;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
		if(myteam==oponent)
			SameTeam();
}
function OponentNewZealand(){
    oponent="New Zealand";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=true;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
		if(myteam==oponent)
			SameTeam();
}
function OponentAustralia(){
    oponent="Australia";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=false;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=true;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
		if(myteam==oponent)
			SameTeam();
}
function OponentUnitedStates(){
    oponent="United States";
    IndiaPanel1.gameObject.active=false;
    UnitedStatesPanel1.gameObject.active=true;
    EnglandPanel1.gameObject.active=false;
    AustraliaPanel1.gameObject.active=false;
    SouthAfricaPanel1.gameObject.active=false;
    NewZealandPanel1.gameObject.active=false;
    PakistanPanel1.gameObject.active=false;
    SriLankaPanel1.gameObject.active=false;
		if(myteam==oponent)
			SameTeam();
}


function MyteamIndia(){
    myteam="India";
    IndiaPanel.gameObject.active=true;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamEngland(){
    myteam="England";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=true;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamSriLanka(){
    myteam="Sri Lanka";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=true;
    if(myteam==oponent)
        SameTeam();
}
function MyteamPakisthan(){
    myteam="Pakistan";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=true;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamSouthAfrica(){
    myteam="South Africa";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=true;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamNewZealand(){
    myteam="New Zealand";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=true;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamAustralia(){
    myteam="Australia";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=false;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=true;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}
function MyteamUnitedStates(){
    myteam="United States";
    IndiaPanel.gameObject.active=false;
    UnitedStatesPanel.gameObject.active=true;
    EnglandPanel.gameObject.active=false;
    AustraliaPanel.gameObject.active=false;
    SouthAfricaPanel.gameObject.active=false;
    NewZealandPanel.gameObject.active=false;
    PakistanPanel.gameObject.active=false;
    SriLankaPanel.gameObject.active=false;
    if(myteam==oponent)
        SameTeam();
}


function DifficultyEasy()
{
	difficulty=0;	
}
function DifficultyHard()
{
	difficulty=1;
}

function SuperOver()
{
	overs=1;
}

function TwoOver()
{
	overs=2;
}
function FiveOver()
{
	overs=5;
}


function GabbaStadium()
{
	stadium=1;
}
function otherStadium()
{
	stadium=0;
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

function customJsonBuilder()
{
	var init = "{";
	var end = "}";
	var special = "\"";
	var myTeamKey = "\"myTeam\":";
	var oppTeamKey = "\"opponentTeam\":";
	var difficultyKey = "\"Difficulty\":";
	var oversKey = "\"Overs\":";
	var stadiumKey = "\"Stadium\":";
	var difficultyVal = special + difficulty + special;
	Debug.Log("overs="+overs);
	Debug.Log("myteam="+myteam);
	Debug.Log("oponent="+oponent);
	var oversVal = special + overs + special;
	var stadiumVal = special + stadium + special;
	var myTeam = special + myteam + special;
	var oppTeam = special + oponent + special;
	var jsonConstruct = init + myTeamKey + myTeam + "," + oppTeamKey + oppTeam + "," + difficultyKey + difficultyVal + "," + oversKey + oversVal + "," + stadiumKey + stadiumVal + end;
	return jsonConstruct;
}

function Play(){
	var sw = new StreamWriter("Assets/jsonSettings.txt");
	var json = customJsonBuilder();
	sw.Write(json);
	sw.Close();
	Application.LoadLevel("cricket");
}



function TeamDetailsDisplay()
{
	teamdetails.gameObject.active=true;
	graph1.gameObject.active=false;
	graph2.gameObject.active=false;
}

function graph1Display()
{
	teamdetails.gameObject.active=false;
	graph1.gameObject.active=true;
	graph2.gameObject.active=false;
}


function graph2Display()
{
	teamdetails.gameObject.active=false;
	graph1.gameObject.active=false;
	graph2.gameObject.active=true;
}
