using UnityEngine;
using System.Collections;

//script to control texture scrolling of shops background
public class TextureScroll : MonoBehaviour {

	private const float TEXTURE_SPEED_ORIGIN = 0.065f;
	private const float TEXTURE_SPEED_SCALE = 0.0007f;

	private Renderer rend;

	private float speed;
	private float pos;

	void Start () {
		rend = transform.GetComponent<Renderer> ();
		pos = 0;
	}

	//increase speed of scrolling and scroll texture each frame
	void Update () {
		speed = TEXTURE_SPEED_ORIGIN + (TEXTURE_SPEED_SCALE*Time.timeSinceLevelLoad);
		pos += speed * Time.deltaTime;
		rend.material.SetTextureOffset ("_MainTex", new Vector2 (pos, 0));
	}
}
