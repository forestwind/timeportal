using UnityEngine;
using System.Collections;

public class missile_1 : MonoBehaviour {
	
	public GameObject missileExplosion;

	public AudioClip missileExpClip;
	public AudioClip missileShootingClip;

	public int speed=10000;

	private Transform tr;
	
	Vector3 destination;


	// Use this for initialization
	void Start () {
		destination = GameObject.Find("DroneCamera").GetComponentInChildren<FireCtrl>().m_Hit.point;
		transform.LookAt(destination);
		tr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate ( Vector3.forward * Time.deltaTime * speed);
		
		if(transform.position.z > 1000)
			Destroy(this.gameObject);
		
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "ENEMY") {
			AudioSource.PlayClipAtPoint(missileExpClip, tr.position);

			//GetComponent<Renderer>().enabled = false;
			Destroy(gameObject);
			StartCoroutine(MissileExplosion());
		}
	}

	IEnumerator MissileExplosion()
	{
		Instantiate(missileExplosion, tr.position, Quaternion.identity);

		yield return null;
	}
}
