using UnityEngine;
using UnityEngine.SceneManagement;


//script handling player control and character behaviours
public class PlayerController : MonoBehaviour
{
	
	private const float left = 2.7f;
	private const float right = -2.7f;
	private const float centre = 0;

	public GameObject popUp;

	//set in editor
	public float speed;

	//children of object
	private GameObject Forcefield;
	private GameObject Sparks;

	private Animator anim;

  private Rigidbody rigidbody3d;

  private bool canJump = true;
	private bool poweredUp = false;
	private bool grinding = false;
	private bool bailed = false;
	private bool movingToLeft = false;
	private bool movingToCentre = false;
	private bool movingToRight = false;

	private float poweredUpTime = 0;
	private float bailTime = 4;

  private void Start()
  {
    rigidbody3d = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		//zero z position to insure correct movement control
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
		//zero score at start of level
		ScoreController.score = 0;
		Forcefield = transform.FindChild ("Forcefield").gameObject;
		Sparks = transform.FindChild ("Sparks").gameObject;
  }

  private void Update()
  {
		InputControl ();

		MovementControl ();

		//if invincible reduce timer by time since last frame
		if (poweredUp) 
		{
			poweredUpTime -= Time.deltaTime;
		}

		//if timer has run out deactivate invincibility and hide forcefield
		if (poweredUpTime <= 0) 
		{
			poweredUp = false;
			Forcefield.SetActive (false);
		}

		//if grinding increase score each frame
		if (grinding) 
		{
			ScoreController.score += 1;
		}

		//if you have bailed reduce timer to exit by time since last frame
		if (bailed == true) {	
			bailTime -= Time.deltaTime;
		} 
		//otherwise increase the score
		else 
		{
			ScoreController.score += Time.deltaTime*2;
		}

		//if timer to exit has ended, load end screen
		if (bailTime <= 0) 
		{
			SceneManager.LoadScene ("EndGame");
		}

		//set boolean value in animator in order to play animation
		anim.SetBool ("Jumping?", !(canJump));

  }

	private void InputControl()
	{
		//if you are allowed to jump and the up arrow is pressed then jump
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(canJump == true)
			{
				rigidbody3d.AddForce(new Vector2(0, 750));
				canJump = false;
			}
		}

		//if you aren't already moving, begin moving to a different lane based on current position and key input
		if (!movingToLeft || !movingToRight || !movingToCentre) 
		{
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				if (transform.position.z == right) 
				{
					movingToCentre = true;
				} 
				else if (transform.position.z == centre) 
				{
					movingToLeft = true;
				}
			}

			if (Input.GetKeyDown (KeyCode.RightArrow)) 
			{
				if (transform.position.z == left) 
				{
					movingToCentre = true;
				} 
				else if (transform.position.z == centre) 
				{
					movingToRight = true;
				}
			}
		}
	}

	private void MovementControl()
	{
		//if moving to left translate character by a small amount to the left each frame
		if (movingToLeft == true) 
		{
			transform.Translate (0, 0, -speed * Time.deltaTime);
			//if character has reached the destination stop moving and set position to the exact value
			if (transform.position.z >= left) 
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,left);
				movingToLeft = false;
			}
		}

		//if moving to left translate character by a small amount to the right each frame
		if (movingToRight == true) 
		{
			transform.Translate (0, 0, speed * Time.deltaTime);
			//if character has reached the destination stop moving and set position to the exact value
			if (transform.position.z <= right) 
			{
				transform.position = new Vector3(transform.position.x,transform.position.y,right);
				movingToRight = false;
			}
		}

		//if moving to centre translate character by a small amount based on which side of the centre the character is on
		if (movingToCentre == true) 
		{
			if (transform.position.z > centre) 
			{
				transform.Translate (0, 0, speed * Time.deltaTime);
				//if character has reached the destination stop moving and set position to the exact value
				if (transform.position.z <= centre) 
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y, centre);
					movingToCentre = false;
				}
			} 
			else 
			{
				transform.Translate (0, 0, -speed * Time.deltaTime);
				//if character has reached the destination stop moving and set position to the exact value
				if (transform.position.z >= centre) 
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y, centre);
					movingToCentre = false;
				}
			}
		}
	}

  private void OnCollisionEnter(Collision other)
  {
		//if colliding with the ground then player can jump again
		if (other.gameObject.name == "Path") 
		{
			canJump = true;
		}
		//if the player is invincible and not grinding on a rail, throw the object they collided with out of the way
		else if (poweredUp && !grinding) 
		{
			Rigidbody otherRigid = other.gameObject.GetComponent<Rigidbody> ();
			otherRigid.constraints = RigidbodyConstraints.None;
			otherRigid.AddForce (new Vector3 (0, 750, -500));
		}
		//otherwise if the player is not grinding, start bailed timer, spin character and throw them forward
		else if (!grinding) 
		{
			bailed = true;
			rigidbody3d.constraints = RigidbodyConstraints.None;
			rigidbody3d.AddTorque (new Vector3 (0, 200, 0));
			rigidbody3d.AddForce (new Vector3 (500, 500, 0));
		}
  }

	private void OnTriggerEnter(Collider other)
	{
		//on entering the powerup, make the character invincible, start the timer, show the forcefield, drop a popup and destroy the powerup
		if (other.gameObject.name == "PowerUp(Clone)") {
			poweredUp = true;
			poweredUpTime = 7;
			Forcefield.SetActive (true);
			Instantiate (popUp);
			Destroy (other.gameObject);
		}
		//on entering any other trigger, if the player hasn't bailed add 50 to the score
		else if (!bailed)
		{
			ScoreController.score += 50;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		//if in the trigger above a grind rail and the spacebar is pressed when the player hasn't bailed
		if (other.gameObject.name == "GrindRail(Clone)") 
		{
			if ((Input.GetKeyDown (KeyCode.Space)) && (!bailed)){
				//player is grinding, can jump, is pushed down onto the rail and has its particle emmitter turned on
				grinding = true;
				canJump = true;
				rigidbody3d.AddForce (new Vector3 (0, -500, 0));
				Sparks.SetActive (true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//when exiting the grindrail trigger, the player is not grinding and its particle emmitter is turned off
		if (other.gameObject.name == "GrindRail(Clone)") 
		{
			grinding = false;
			Sparks.SetActive (false);
		}
	}

}
