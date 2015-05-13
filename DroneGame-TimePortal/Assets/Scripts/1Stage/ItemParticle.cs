using UnityEngine;
using System.Collections;

public class ItemParticle : MonoBehaviour {
	public ParticleSystem itemParticle;

	// Use this for initialization
	void Start () {
		//itemParticle = GetComponent<ParticleSystem> ();
		itemParticle.startSize = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
