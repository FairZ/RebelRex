using UnityEngine;
using System.Collections;

//script for controlling the backing music
public class BackingMusic : MonoBehaviour {

	private GameObject[] Objects;

	void Start () {
		//make the object persistent between levels
		DontDestroyOnLoad (this.gameObject);

		//find all objects with the music tag and if there is already one, destroy this object
		Objects = GameObject.FindGameObjectsWithTag ("Music");

		if (Objects.Length == 2) {
			Destroy (this.gameObject);
		}
	}

}
