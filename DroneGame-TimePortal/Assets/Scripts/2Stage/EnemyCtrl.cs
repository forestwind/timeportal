using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour {

	public float speed;
	public GameObject explosion;

	public AudioClip SpaceShipExp_small;

	private int hitCount = 0;
	private Transform tr;
	private GameManager gameMgr;

	AudioSource audio;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		audio = GetComponent<AudioSource> ();
		GetComponent<Rigidbody>().AddForce(0,0,-speed);

	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "MISSILE") {
			if(++hitCount >= 2)	{
				StartCoroutine(ExplosionEnemy());
			}
		}

	}

	IEnumerator ExplosionEnemy()
	{
		Instantiate (explosion, tr.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (SpaceShipExp_small, tr.position);
		Destroy (gameObject);
		gameMgr.Score += 10;

		yield return null;

	}


}
