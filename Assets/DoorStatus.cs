/**
 * Manages status and visuals for a single trapdoor
**/

using UnityEngine;
using System.Collections;

public class DoorStatus : MonoBehaviour {

	private int status = 0; // 0 = standard, 1 = chosen, 2 = open, 3 = lost

	GameObject light;
	GameObject door;

	bool open; // if the door is open or closed

	// Use this for initialization
	void Start () {
		light = transform.Find("Spotlight").gameObject;
		door = transform.Find("Door").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setStatus(int statusNew) {
		//Debug.Log("set status: " + statusNew);
		switch (statusNew) {
		// standard
		case 0: light.SetActive(true);
				light.GetComponent<Light>().intensity = 1;
				closeDoor();
			    break;
	 	// chosen
		case 1: light.SetActive(true);
				light.GetComponent<Light>().intensity = 2;
				break;
		// open
		case 2: openDoor();
				break;
		// lost
		case 3: light.SetActive(false);
				break;
		}
		status = statusNew;
	}

	public int getStatus() {
		return status;
	}

	// close door if open
	private void closeDoor() {
		if (open) {
			door.transform.Rotate(new Vector3(90f, 0f, 0f));
			open = !open;
		}
	}

	// open door if closed
	private void openDoor() {
		if (!open) {
			door.transform.Rotate(new Vector3(-90f, 0f, 0f));
			open = !open;
		}
	}

}
