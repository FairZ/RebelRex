using UnityEngine;
using System.Collections;

//script for spawning in non player objects
public class Spawn : MonoBehaviour {

	private const float SPAWN_TIME_SCALE = 0.01f;

	//objects assigned in editor
	public GameObject Rail;
	public GameObject Bin;
	public GameObject RoadMarking;
  public GameObject PowerUp;
	public GameObject GrindRail;

	private int spawnState;

	private float obstacleSpawnTime;
	private float scenerySpawnTime;
  private float powerUpSpawnTime;

  void Start () {
		spawnState = 1;
		obstacleSpawnTime = 1.5f - (SPAWN_TIME_SCALE*Time.timeSinceLevelLoad);
		scenerySpawnTime = 0;
    powerUpSpawnTime = Random.Range(20f, 40f);

  }

	void Update () {
		//reduce all timers each frame
		obstacleSpawnTime -= Time.deltaTime;
		scenerySpawnTime -= Time.deltaTime;
    powerUpSpawnTime -= Time.deltaTime;

		//if obstacle spawn timer is finished spawn a random configuration of obsticles and start another timer
    if (obstacleSpawnTime <= 0) 
		{
			//assign spawn timer length based on time into level up to a limit
			obstacleSpawnTime = 1.5f - (SPAWN_TIME_SCALE*Time.timeSinceLevelLoad);
			if (obstacleSpawnTime < 0.5f) 
			{
				obstacleSpawnTime = 0.5f;
			}

			//if the previous spawn state was a railing don't spawn another railing 
			//assign spawnState within range
			if (spawnState == 6) 
			{
				spawnState = Random.Range (0, 6);
			} 
			else 
			{
				spawnState = Random.Range (0, 7);
			}

			//spawn objects based on spawnState
			switch (spawnState) 
			{
			case 0:
				Instantiate (Bin, new Vector3 (35, 0, -2.7f), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			case 1:
				Instantiate (Bin, new Vector3 (35, 0, 0), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			case 2:
				Instantiate (Bin, new Vector3 (35, 0, 2.7f), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			case 3:
				Instantiate (Bin, new Vector3 (35, 0, -2.7f), Quaternion.Euler(new Vector3(0,-90,0)));
				Instantiate (Bin, new Vector3 (35, 0, 0), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			case 4:
				Instantiate (Bin, new Vector3 (35, 0, 0), Quaternion.Euler(new Vector3(0,-90,0)));
				Instantiate (Bin, new Vector3 (35, 0, 2.7f), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			case 5:
				Instantiate (GrindRail, new Vector3 (35, 0, 0), Quaternion.identity);
				break;
			case 6:
				Instantiate (Rail, new Vector3 (35, 0, 0), Quaternion.Euler(new Vector3(0,-90,0)));
				break;
			}
		}

		//if scenery spawn timer is complete spawn another road marking and start another timer
		if (scenerySpawnTime <= 0) 
		{
			scenerySpawnTime = 2.0f - (SPAWN_TIME_SCALE*Time.timeSinceLevelLoad);
			if (scenerySpawnTime < 0.5f) 
			{
				scenerySpawnTime = 0.5f;
			}

			Instantiate (RoadMarking, new Vector3 (40, 0, -14.5f), Quaternion.identity);
		}

		//if powerup spawn timer is complete spawn a powerup and assign a new timer
    if (powerUpSpawnTime <= 0)
  	{
    	powerUpSpawnTime = Random.Range(20f, 40f);
      Instantiate(PowerUp, new Vector3(35, 1, 0), Quaternion.identity);
    }
	}
}
