﻿/**
 * Creates the playfield and controls the game interaction
**/

using UnityEngine;
using System.Collections.Generic;

public class Control : MonoBehaviour {

	public Transform doorPrefab;
	public Transform characterPrefab;

	// saves all trapdoor objects
	private string[] allDoorsKeys;
	private GameObject[] allDoorsValues;
	private GameObject[] allDoorCharacters;
	private int allDoorsCounter;

	GameObject cam1;
	GameObject cam2;
	GameObject overview;

	bool camToggle;

	// initialization
	void Start () {

		allDoorsKeys = new string[64];
		allDoorsValues = new GameObject[64];
		allDoorCharacters = new GameObject[64];
		allDoorsCounter = 0;

		cam1 = GameObject.Find("MainCameraP1");
		cam2 = GameObject.Find("MainCameraP2");
		overview = GameObject.Find("OverviewCAM");
		overview.SetActive(false);
		bool camToggle = false;
		

		Transform currentDoor;

		List<GameObject> models1 = transform.GetComponent<CharacterLoad>().getListOfCharacters(32);
		List<GameObject> models2 = transform.GetComponent<CharacterLoad>().getListOfCharacters(32);

		// instantiate field of trapdoors
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				currentDoor = (Transform) Instantiate(doorPrefab,
				                                      new Vector3((float) i<=3 ? i*5 : 5+i*5, 0f, (float) j*5),
				                          Quaternion.identity);

				// save trapdoor data for later
				allDoorsKeys[allDoorsCounter] = i.ToString() + j.ToString();
				allDoorsValues[allDoorsCounter] = currentDoor.gameObject;
				allDoorsCounter++;

				// set trapdoor colors for 2 players
				if (i < 4) {
					currentDoor.Find("Door").Find("Leaf").gameObject.renderer.material.color = new Color(0.6f, 0.6f, 0);
				} else {
					currentDoor.Find("Door").Find("Leaf").gameObject.renderer.material.color = new Color(0, 0.6f, 0.6f);
				}

				// Instantiate Character
				if((i*8+j)<32){
				allDoorCharacters[i*8+j] = Instantiate(models1[(i*8+j)],
					                                       new Vector3((float) i<=3 ? i*5 : 5+i*5, 15f, (float) j*5),
					                                       Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
				} else {
					allDoorCharacters[i*8+j] = Instantiate(models2[(i*8+j)-32],
					                                       new Vector3((float) i<=3 ? i*5 : 5+i*5, 15f, (float) j*5),
						                                       Quaternion.Euler(0.0f, 270.0f, 0.0f)) as GameObject;
				}
				
			}
		}
		
	}
	
	// TODO Right half of field doesn't really fit yet and has some symbols missing

	// Update is called once per frame
	void Update () {
		//for(int i = 0; i < allDoorCharacters.Length; i++){
		//	allDoorCharacters[i].transform.Rotate(new Vector3(allDoorCharacters[i].transform.rotation.x, allDoorCharacters[i].transform.rotation.y + 15.0f * Time.deltaTime, allDoorCharacters[i].transform.rotation.z));
		//}
		if (Input.GetKeyDown (KeyCode.Space)) {
			camToggle = !camToggle;
			/*cam1.GetComponent<Camera>().enabled = camToggle;
			cam2.GetComponent<Camera>().enabled = camToggle;
			overview.GetComponent<Camera>().enabled = !camToggle;*/
			cam1.SetActive(camToggle);
			cam2.SetActive(camToggle);
			overview.SetActive(!camToggle);
		} else if (Input.GetKeyDown (KeyCode.B)) {
			chooseDoor("00");
		} else if (Input.GetKeyDown (KeyCode.G)) {
			chooseDoor("01");
		} else if (Input.GetKeyDown (KeyCode.T)) {
			chooseDoor("02");
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			chooseDoor("03");
		} else if (Input.GetKeyDown (KeyCode.Y)) {
			chooseDoor("04");
		} else if (Input.GetKeyDown (KeyCode.A)) {
			chooseDoor("05");
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			chooseDoor("06");
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			chooseDoor("07");
		} else if (Input.GetKeyDown (KeyCode.N)) {
			chooseDoor("10");
		} else if (Input.GetKeyDown (KeyCode.H)) {
			chooseDoor("11");
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			chooseDoor("12");
		} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
			chooseDoor("13");
		} else if (Input.GetKeyDown (KeyCode.X)) {
			chooseDoor("14");
		} else if (Input.GetKeyDown (KeyCode.S)) {
			chooseDoor("15");
		} else if (Input.GetKeyDown (KeyCode.W)) {
			chooseDoor("16");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			chooseDoor("17");
		} else if (Input.GetKeyDown (KeyCode.M)) {
			chooseDoor("20");
		} else if (Input.GetKeyDown (KeyCode.J)) {
			chooseDoor("21");
		} else if (Input.GetKeyDown (KeyCode.U)) {
			chooseDoor("22");
		} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
			chooseDoor("23");
		} else if (Input.GetKeyDown (KeyCode.C)) {
			chooseDoor("24");
		} else if (Input.GetKeyDown (KeyCode.D)) {
			chooseDoor("25");
		} else if (Input.GetKeyDown (KeyCode.E)) {
			chooseDoor("26");
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			chooseDoor("27");
		} else if (Input.GetKeyDown (KeyCode.Comma)) {
			chooseDoor("30");
		} else if (Input.GetKeyDown (KeyCode.K)) {
			chooseDoor("31");
		} else if (Input.GetKeyDown (KeyCode.I)) {
			chooseDoor("32");
		} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
			chooseDoor("33");
		} else if (Input.GetKeyDown (KeyCode.V)) {
			chooseDoor("34");
		} else if (Input.GetKeyDown (KeyCode.F)) {
			chooseDoor("35");
		} else if (Input.GetKeyDown (KeyCode.R)) {
			chooseDoor("36");
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			chooseDoor("37");
		} else if (Input.GetKeyDown (KeyCode.Keypad3)) {
			chooseDoor("40");
		} else if (Input.GetKeyDown (KeyCode.Keypad5)) {
			chooseDoor("41");
		} else if (Input.GetKeyDown (KeyCode.Keypad7)) {
			chooseDoor("42");
		} else if (Input.GetKeyDown (KeyCode.Slash)) {
			chooseDoor("43");
		} else if (Input.GetKeyDown (KeyCode.Semicolon)) {
			chooseDoor("44");
		} else if (Input.GetKeyDown (KeyCode.L)) {
			chooseDoor("45");
		} else if (Input.GetKeyDown (KeyCode.O)) {
			chooseDoor("46");
		} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
			chooseDoor("47");
		} else if (Input.GetKeyDown (KeyCode.Keypad0)) {
			chooseDoor("50");
		} else if (Input.GetKeyDown (KeyCode.Keypad6)) {
			chooseDoor("51");
		} else if (Input.GetKeyDown (KeyCode.Keypad8)) {
			chooseDoor("52");
		} else if (Input.GetKeyDown (KeyCode.Asterisk)) {
			chooseDoor("53");
		} else if (Input.GetKeyDown (KeyCode.Period)) {
			chooseDoor("54");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) { // ö
			chooseDoor("55");
		} else if (Input.GetKeyDown (KeyCode.P)) {
			chooseDoor("56");
		} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			chooseDoor("57");
		} else if (Input.GetKeyDown (KeyCode.Comma)) { // num
			chooseDoor("60");
		} else if (Input.GetKeyDown (KeyCode.Keypad1)) {
			chooseDoor("61");
		} else if (Input.GetKeyDown (KeyCode.Keypad9)) {
			chooseDoor("62");
		} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			chooseDoor("63");
		} else if (Input.GetKeyDown (KeyCode.Colon)) { 
			chooseDoor("64");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) { // ä
			chooseDoor("65");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) { // ü
			chooseDoor("66");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) { // ß
			chooseDoor("67");
		} else if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			chooseDoor("70");
		} else if (Input.GetKeyDown (KeyCode.Keypad2)) {
			chooseDoor("71");
		} else if (Input.GetKeyDown (KeyCode.Keypad4)) {
			chooseDoor("72");
		} else if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			chooseDoor("73");
		} else if (Input.GetKeyDown (KeyCode.Minus)) {
			chooseDoor("74");
		} else if (Input.GetKeyDown (KeyCode.Hash)) {
			chooseDoor("75");
		} else if (Input.GetKeyDown (KeyCode.Plus)) {
			chooseDoor("76");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) { // Apostroph
			chooseDoor("77");
		}

	}
	string lastKey = "";
	// evoke action on a trapdoor
	private void chooseDoor(string key) {

		if(lastKey.Length>0 && lastKey != key) allDoorsValues[getPosition(lastKey)].GetComponent<DoorStatus>().setStatus(0);
		DoorStatus doorScript = allDoorsValues[getPosition(key)].GetComponent<DoorStatus>();

		// TODO real gaming interaction, currently only demo of all states

		if (doorScript.getStatus() == 0) {
			doorScript.setStatus(1);
		} else if (doorScript.getStatus() == 1) {
			doorScript.setStatus(3);
		} else if (doorScript.getStatus() == 3) {
			doorScript.setStatus(2);
			playSound(key);
		} else if (doorScript.getStatus() == 2) {
			doorScript.setStatus(0);
		}
		lastKey = key;
	}

	private void playSound(string key){
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("charSounds/" + allDoorCharacters[getPosition(key)].name.Split('(')[0])as AudioClip , new Vector3(0.0f, 0.0f, 0.0f));
	}

	// Get the Position of a key in the allDoorsKeys-Array
	private int getPosition(string key) {
		for (int i = 0; i < allDoorsKeys.Length; i++) {
			if (allDoorsKeys[i] == key) {
				return i;
			}
		}
		return -1;
	}

}
