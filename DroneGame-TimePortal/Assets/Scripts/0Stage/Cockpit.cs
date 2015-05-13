using UnityEngine;
using System.Collections;

public class Cockpit : MonoBehaviour {

	public AudioClip Stage0Clear; 

	IEnumerator OnTriggerEnter(Collider coll)
	{
		if (coll.GetComponent<Collider> ().tag == "Player") {
			Debug.Log("Enter~!");
		}
		GameObject.Find("UI").GetComponent<GameUI>().isclear = true;

		GetComponent<AudioSource>().PlayOneShot(Stage0Clear);

		yield return new WaitForSeconds(5.0f);

		Application.LoadLevel ("1Stage");

	}
}
