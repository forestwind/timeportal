using UnityEngine;
using System.Collections;

public class EnemyCtrl_1 : MonoBehaviour {

	public float speed;
	public GameObject explosion;
	public AudioClip SpaceShipExp_big;

	private int hitCount = 0;
	private Transform tr;
	private GameManager gameMgr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().AddForce(0,0,-speed);
	}
	
	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "MISSILE") {
			//Destroy(coll.gameObject);

			if(++hitCount >= 4)
			{
				StartCoroutine(ExplosionEnemy());
			}
			
		}
		
	}
	
	IEnumerator ExplosionEnemy()
	{
		Instantiate (explosion, tr.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (SpaceShipExp_big, tr.position);
		Destroy (gameObject, 0.1f);
		gameMgr.Score += 30;

		yield return null;
		
	}
}
