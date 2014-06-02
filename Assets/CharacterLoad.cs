using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CharacterLoad : MonoBehaviour {
	

	public List<GameObject> characterModels;
	
	// Use this for initialization
	void Start () {
		characterModels = new List<GameObject>();
		GameObject[] loadedGameObjects = Resources.LoadAll<GameObject>("charPrefabs");
		for(int i = 0; i < loadedGameObjects.Length; i++){
			characterModels.Add(loadedGameObjects[i]);
			//characterModels[i].transform.localScale = new Vector3(0.0002f, 0.0002f, 0.002f);
			if(characterModels[i].GetComponent<Rigidbody>() == null){
				Rigidbody toAdd = characterModels[i].AddComponent<Rigidbody>();
				toAdd.mass = 50;
			}
			if(characterModels[i].GetComponent<BoxCollider>() == null){
				BoxCollider toAdd = characterModels[i].AddComponent<BoxCollider>();
			}
		}
	}

	public List<GameObject> getListOfCharacters(int count){
		Start ();
		List<GameObject> output = new List<GameObject>();
		List<GameObject> newList = new List<GameObject>(characterModels);
		for(int i = 0; i < count; i++){
			int index = Random.Range(0, newList.Count);
			//newList[index].transform.Rotate(new Vector3(0.0f, rotAngle, 0.0f));
			output.Add(newList[index]);
			newList.RemoveAt(index);
		}
		return output;
	}

	// Update is called once per frame
	void Update () {
		
	}
}