using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {

	public MovieTexture movietexture;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.mainTexture = movietexture;
		movietexture.Play();
		movietexture.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
