/**
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

	private float roundCounter=5;
	private int roundNumber=1;
	private bool isPaused=true, roundHasWinner=false;

	private HashSet<GameObject> quizKeys;
	private GameObject currentCharacter;
	private int player1Points=32, player2Points=32;


	private const float roundCounterDefault=12, pauseCounterDefault=5;
	private GUIText infoText, scorePlayer1, scorePlayer2;

	GameObject cam1;
	GameObject cam2;
	GameObject overview;

	bool camToggle;

	// initialization
	void Start () {
	
		quizKeys = new HashSet<GameObject>();

		allDoorsKeys = new string[64];
		allDoorsValues = new GameObject[64];
		allDoorCharacters = new GameObject[64];
		allDoorsCounter = 0;

		cam1 = GameObject.Find("MainCameraP1");
		cam2 = GameObject.Find("MainCameraP2");
		overview = GameObject.Find("OverviewCAM");
		overview.SetActive(false);
		bool camToggle = false;


		infoText = (GUIText) GameObject.Find("RoundCountdown").GetComponent("GUIText");
		scorePlayer1 = (GUIText) GameObject.Find("Player1").GetComponent("GUIText");
		scorePlayer2 = (GUIText) GameObject.Find("Player2").GetComponent("GUIText");

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

	// Update is called once per frame
	void Update () {

		// Winning Condition 
		if(quizKeys.Count==32 || player1Points==0 || player2Points==0) {

			if(player1Points>player2Points) {
				((TextMesh) GameObject.Find("Player2Wins").GetComponent("TextMesh")).text="Player 2 Wins";
			} else {
				((TextMesh) GameObject.Find("Player1Wins").GetComponent("TextMesh")).text="Player 1 Wins";
			}

			if( (Input.GetKeyDown (KeyCode.KeypadDivide)) ||  (Input.GetKeyDown (KeyCode.KeypadEnter)) ||
			    (Input.GetKeyDown (KeyCode.Y)) ||   (Input.GetKeyDown (KeyCode.Comma)) ) {
				Application.LoadLevel (Application.loadedLevel);
			}

		}

		else {

			if(isPaused) {
				infoText.text= "Round " + roundNumber.ToString();
			} else {
				infoText.text= Mathf.Floor(roundCounter).ToString();
			}

			//32 rounds with short pauses between
			if(roundCounter<0.1) {
				if(isPaused) {
					roundCounter=roundCounterDefault;

					// play random sound & save it
					currentCharacter = allDoorCharacters[Random.Range(0, 31)];
					while(quizKeys.Contains(currentCharacter)) {
						currentCharacter = allDoorCharacters[Random.Range(0, 31)];
					}

					quizKeys.Add(currentCharacter);
					playSoundByName(currentCharacter.gameObject.name);

				} else {
					roundCounter=pauseCounterDefault;
					roundNumber++;

					// check who has won
					if(!roundHasWinner) {
						removeCharacter(true);
						removeCharacter(false);
					}

				}
				isPaused=!isPaused;
			} 


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
			} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
				chooseDoor("00");
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				chooseDoor("01");
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				chooseDoor("02");
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				chooseDoor("03");
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				chooseDoor("04");
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				chooseDoor("05");
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				chooseDoor("06");
			} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				chooseDoor("07");
			} else if (Input.GetKeyDown (KeyCode.Q)) {
				chooseDoor("10");
			} else if (Input.GetKeyDown (KeyCode.W)) {
				chooseDoor("11");
			} else if (Input.GetKeyDown (KeyCode.E)) {
				chooseDoor("12");
			} else if (Input.GetKeyDown (KeyCode.R)) {
				chooseDoor("13");
			} else if (Input.GetKeyDown (KeyCode.T)) {
				chooseDoor("14");
			} else if (Input.GetKeyDown (KeyCode.Z)) {
				chooseDoor("15");
			} else if (Input.GetKeyDown (KeyCode.U)) {
				chooseDoor("16");
			} else if (Input.GetKeyDown (KeyCode.I)) {
				chooseDoor("17");
			} else if (Input.GetKeyDown (KeyCode.A)) {
				chooseDoor("20");
			} else if (Input.GetKeyDown (KeyCode.S)) {
				chooseDoor("21");
			} else if (Input.GetKeyDown (KeyCode.D)) {
				chooseDoor("22");
			} else if (Input.GetKeyDown (KeyCode.F)) {
				chooseDoor("23");
			} else if (Input.GetKeyDown (KeyCode.G)) {
				chooseDoor("24");
			} else if (Input.GetKeyDown (KeyCode.H)) {
				chooseDoor("25");
			} else if (Input.GetKeyDown (KeyCode.J)) {
				chooseDoor("26");
			} else if (Input.GetKeyDown (KeyCode.K)) {
				chooseDoor("27");
			} else if (Input.GetKeyDown (KeyCode.Y)) {
				chooseDoor("30");
			} else if (Input.GetKeyDown (KeyCode.X)) {
				chooseDoor("31");
			} else if (Input.GetKeyDown (KeyCode.C)) {
				chooseDoor("32");
			} else if (Input.GetKeyDown (KeyCode.V)) {
				chooseDoor("33");
			} else if (Input.GetKeyDown (KeyCode.B)) {
				chooseDoor("34");
			} else if (Input.GetKeyDown (KeyCode.N)) {
				chooseDoor("35");
			} else if (Input.GetKeyDown (KeyCode.M)) {
				chooseDoor("36");
			} else if (Input.GetKeyDown (KeyCode.Comma)) {
				chooseDoor("37");
			} else if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
				chooseDoor("40");
			} else if (Input.GetKeyDown (KeyCode.KeypadPeriod)) {
				chooseDoor("41");
			} else if (Input.GetKeyDown (KeyCode.Keypad0)) {
				chooseDoor("42");
			} else if (Input.GetKeyDown (KeyCode.Keypad3)) {
				chooseDoor("43");
			} else if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
				chooseDoor("44");
			} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
				chooseDoor("45");
			} else if (Input.GetKeyDown (KeyCode.KeypadMultiply)) {
				chooseDoor("46");
			} else if (Input.GetKeyDown (KeyCode.KeypadDivide)) {
				chooseDoor("47");
			} else if (Input.GetKeyDown (KeyCode.Keypad2)) {
				chooseDoor("50");
			} else if (Input.GetKeyDown (KeyCode.Keypad1)) {
				chooseDoor("51");
			} else if (Input.GetKeyDown (KeyCode.Keypad6)) {
				chooseDoor("52");
			} else if (Input.GetKeyDown (KeyCode.Keypad5)) {
				chooseDoor("53");
			} else if (Input.GetKeyDown (KeyCode.Keypad4)) {
				chooseDoor("54");
			} else if (Input.GetKeyDown (KeyCode.Keypad9)) { 
				chooseDoor("55");
			} else if (Input.GetKeyDown (KeyCode.Keypad8)) {
				chooseDoor("56");
			} else if (Input.GetKeyDown (KeyCode.Keypad7)) {
				chooseDoor("57");
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) { // num
				chooseDoor("60");
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				chooseDoor("61");
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				chooseDoor("62");
			} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
				chooseDoor("63");
			} else if (Input.GetKeyDown (KeyCode.F12)) { 
				chooseDoor("64");
			} else if (Input.GetKeyDown (KeyCode.F11)) { // ä
				chooseDoor("65");
			} else if (Input.GetKeyDown (KeyCode.F10)) { // ü
				chooseDoor("66");
			} else if (Input.GetKeyDown (KeyCode.F9)) { // ß
				chooseDoor("67");
			} else if (Input.GetKeyDown (KeyCode.F8)) {
				chooseDoor("70");
			} else if (Input.GetKeyDown (KeyCode.F7)) {
				chooseDoor("71");
			} else if (Input.GetKeyDown (KeyCode.F6)) {
				chooseDoor("72");
			} else if (Input.GetKeyDown (KeyCode.F5)) {
				chooseDoor("73");
			} else if (Input.GetKeyDown (KeyCode.F4)) {
				chooseDoor("74");
			} else if (Input.GetKeyDown (KeyCode.F3)) {
				chooseDoor("75");
			} else if (Input.GetKeyDown (KeyCode.F2)) {
				chooseDoor("76");
			} else if (Input.GetKeyDown (KeyCode.F1)) { // Apostroph
				chooseDoor("77");
			}


			// GUI Text
			roundCounter-=Time.deltaTime;
			
			scorePlayer1.text=player1Points.ToString() + " Left";
			scorePlayer2.text=player2Points.ToString() + " Left";

		}
	}

	string lastKeyP1 = "", lastKeyP2 = "";


	/**
	 * Selects a door and changes its status
	 * Keys higher than 40 -> player2
	 */
	private void chooseDoor(string key) {

		string lastKey = lastKeyP2;
		bool isPlayer1=false;
		if(int.Parse(key) >= 40) {
			isPlayer1=true;
			lastKey = lastKeyP1;
		}


		DoorStatus doorScript = allDoorsValues[getPosition(key)].GetComponent<DoorStatus>();

		if(doorScript.getStatus()!=3) {

			//unselect door
			if(lastKey.Length>0 && lastKey != key) {
				if(allDoorsValues[getPosition(lastKey)].GetComponent<DoorStatus>().getStatus()!=3)
					allDoorsValues[getPosition(lastKey)].GetComponent<DoorStatus>().setStatus(0);
			}

			if (doorScript.getStatus() == 0) {
				doorScript.setStatus(1);
				lastKey = key;

			} else if (doorScript.getStatus() == 1) {

				lastKey = "";

				// right choice
				if(allDoorCharacters[getPosition(key)].name == currentCharacter.gameObject.name) {
					roundHasWinner=true;
					roundCounter=0;
					removeCharacter(isPlayer1);

				// wrong choice
				} else {
					if(isPlayer1)
						player1Points--;
					else
						player2Points--;

					doorScript.setStatus(3);
					playSound(key);

				}


			}

		}

		if(isPlayer1)
			lastKeyP1 = key;
		else
			lastKeyP2 = key;

	}

	/**
	 * checks if the opponent still has this character and removes it 
	 **/
	private void removeCharacter(bool removeFromPlayer1) {
		int add=0;

		if(!removeFromPlayer1) {
			add=32;
		}

		for(int i=0+add; i<(32+add); i++) {

			if(allDoorCharacters[i].name == currentCharacter.gameObject.name) {
				DoorStatus doorScript = allDoorsValues[i].GetComponent<DoorStatus>();
				doorScript.setStatus(3);

				if(!removeFromPlayer1)
					player1Points--;
				else
					player2Points--;
			}
		}
	}

	private void playSound(string key){
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("charSounds/" + allDoorCharacters[getPosition(key)].name.Split('(')[0])as AudioClip , new Vector3(0.0f, 0.0f, 0.0f));
	}

	private void playSoundByName(string name){
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("charSounds/" + name.Split('(')[0])as AudioClip , new Vector3(0.0f, 0.0f, 0.0f));
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
