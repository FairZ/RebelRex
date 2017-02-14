using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//script for controlling score
public class ScoreController : MonoBehaviour {

	//public static variable used to allow acces across scenes and by any object
	public static float score;

	private Text text;

	void Start () {
		text = GetComponent<Text>();
	}

	//update score text to show actual score
	void Update () {
		text.text = "SCORE: " + (int)score;
	}
}
