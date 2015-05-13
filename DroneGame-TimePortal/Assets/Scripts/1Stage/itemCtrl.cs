using UnityEngine;
using System.Collections;

public class itemCtrl : MonoBehaviour {

	float _time;
	// Use this for initialization
	IEnumerator Start () {
		iTween.MoveBy(gameObject, iTween.Hash("y", -16, "easeType", iTween.EaseType.linear));

		yield return new WaitForSeconds (5.0f);

		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("ItemBluePath"), "time", 7));


		yield return new WaitForSeconds (5.0f);

		GameObject.Find("UI").GetComponent<GameUI>().isclear = true;
		GameObject.Find("GameManager").GetComponent<GameManager>().CurrentStage = 2;
		GameObject.Find("GameManager").GetComponent<GameManager>().isStageStart=false;
		GameObject.Find ("GameManager").GetComponent<GameManager> ().GameTime = 100.0f;

		//iTween.MoveBy(gameObject, iTween.Hash("x", -16, "easeType", iTween.EaseType.linear));
	}
	
	// Update is called once per frame
	void Update () {


	}



		//iTween.MoveBy(gameObject, iTween.Hash("y", 100, "easeType", iTween.EaseType.linear));


}
