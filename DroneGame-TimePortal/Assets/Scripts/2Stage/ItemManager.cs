using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	public Transform [] points;
	public int idx;
	public GameObject itemEnemy;
	
	float itemTime = 0.0f;
	float creatTime = 5.0f;

	bool isItemEnemyCreate = false;

	// Use this for initialization
	void Start () {
		
		points = GameObject.Find ("ItemEnemyPath").GetComponentsInChildren<Transform> ();
		idx = UnityEngine.Random.Range (1, points.Length);
		StartCoroutine (itemEnemyCreate());
	}
	
	// Update is called once per frame
	IEnumerator itemEnemyCreate()
	{
		yield return new WaitForSeconds(7.0f);
		//Debug.Log (idx);
		Instantiate (itemEnemy, points [idx].position, Quaternion.identity);
	}
}
