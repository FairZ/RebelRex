using UnityEngine;
using System.Collections;

//script to control non player objects
public class NonPlayerObjectBehaviour : MonoBehaviour {

	private const float SPEED_SCALE = 0.1f;

	private float speed;

	void Start () {
		//speed set based on how long the player has survived to provide increasing difficulty
		speed = -10 - (SPEED_SCALE*Time.timeSinceLevelLoad);
	}

	void Update () {
		//translate object each frame
		transform.Translate(speed * Time.deltaTime, 0, 0,Space.World);
	}
}
