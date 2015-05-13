using UnityEngine;
using System.Collections;

public class BoxCtrl : MonoBehaviour {

	//public GameObject item;
	private itemCtrl items;
	

	// Use this for initialization
	void Awake () {

		items = GameObject.Find ("Item_Blue").GetComponent<itemCtrl> ();
		items.enabled = false;

	}
	
	IEnumerator itemUp(){
		yield return new WaitForSeconds(2.0f);
		items.enabled = true;
	}

}
