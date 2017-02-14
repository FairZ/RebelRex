using UnityEngine;
using System.Collections;

//script controlling out of bounds killzones
public class OutOfBounds : MonoBehaviour {
	//if anything but the player character enters the killzone, destroy it
	void OnTriggerStay(Collider other)
	{
		if (other.name != "Trex") 
		{	
			//if the object has a parent destroy the parent otherwise destroy the object
			if (other.transform.parent != null) 
			{
				Destroy (other.transform.parent.gameObject);
			} 
			else 
			{
				Destroy (other.gameObject);
			}
		}
	}
}

//Note: the parent destruction is needed as some prefabs use a parent as a "wrapper" in order to preserve animations