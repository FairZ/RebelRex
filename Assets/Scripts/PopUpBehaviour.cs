using UnityEngine;
using System.Collections;

//script for controlling invincible pop-up
public class PopUpBehaviour : MonoBehaviour {

	void Start () {
		//push popup down when it is spawned in order to get it accross the screen faster
		transform.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -200, 0));
	}
}
