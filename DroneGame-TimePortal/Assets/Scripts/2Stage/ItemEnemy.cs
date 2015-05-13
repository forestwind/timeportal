using UnityEngine;
using System.Collections;

public class ItemEnemy : MonoBehaviour {


	public GameObject explosion;

	private Transform tr;


	// Use this for initialization
	void Start () {
		int idx = GameObject.Find ("GameManager").GetComponent<ItemManager> ().idx;

		tr = GetComponent<Transform> ();

		Debug.Log (idx);

		if (idx == 1) {
			iTween.MoveTo (gameObject, iTween.Hash ("path", 
			                                        iTweenPath.GetPath ("ItemEnemyPath1"), 
			                                        "time", 30));
		} else if (idx == 2) {
			iTween.MoveTo (gameObject, iTween.Hash ("path", 
			                                        iTweenPath.GetPath ("ItemEnemyPath2"), 
			                                        "time", 30));
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "MISSILE") {
			StartCoroutine(ExplosionEnemy());
			GameObject.Find("DroneCamera").GetComponentInChildren<FireCtrl>().missileLevel = 2;
		}
	}

	IEnumerator ExplosionEnemy()
	{
		Instantiate (explosion, tr.position, Quaternion.identity);
		
		Destroy (gameObject, 0.1f);
		
		yield return null;
		
	}	

}