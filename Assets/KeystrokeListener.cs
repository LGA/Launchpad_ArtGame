using UnityEngine;
using System.Collections;

public class KeystrokeListener : MonoBehaviour {


	public GameObject simpleSphere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Keycodes http://docs.unity3d.com/ScriptReference/KeyCode.html

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			simpleSphere.rigidbody.AddForce (0, 0, -10);
			Debug.Log("Key: 2");
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			simpleSphere.rigidbody.AddForce (10, 0, 0);
			Debug.Log("Key: Q");
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			simpleSphere.rigidbody.AddForce (0, 0, 10);
			Debug.Log("Key: S");
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			simpleSphere.rigidbody.AddForce (-10, 0, 0);
			Debug.Log("Key: E");
		}

	}
}
