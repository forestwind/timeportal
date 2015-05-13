using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {
	public GameObject plasmabeam;
	//public Transform firePos;
	public float beam;

	private int currentStage;
	//public AudioClip razerClip;
	//public AudioClip razerHitClip;

	public GameObject bullet;
	public RaycastHit m_Hit;

	public int missileLevel;

	void Start(){
		plasmabeam = GameObject.Find ("plasmabeam");
		currentStage=GameObject.Find("GameManager").GetComponent<GameManager>().CurrentStage;
	}


	// Update is called once per frame
	void Update () {

		if(currentStage == 1  && GameObject.Find("GameManager").GetComponent<GameManager>().isStageStart == true ){

			if (Input.GetMouseButtonDown (0)) {
				plasmabeam.GetComponent<AudioSource>().Play();
				GetComponent<F3DBeam> ().MaxBeamLength = 1000.0f;
				plasmabeam.transform.Translate(0, 0, 11);
			}
			else if (Input.GetMouseButtonUp(0)) {
				plasmabeam.GetComponent<AudioSource>().Stop();
				GetComponent<F3DBeam>().MaxBeamLength = 0.0f;
				plasmabeam.transform.Translate(0, 0, -11);
			}
		}

		if(GameObject.Find("GameManager").GetComponent<GameManager>().CurrentStage == 2){
		if ( Input.GetMouseButtonDown(0)){
			
			Ray ray = GameObject.Find("DroneCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			
			if ( Physics.Raycast(ray,out m_Hit,100000)) {
				
				//if( m_Hit.collider.tag == "WALL"){
				//GameObject.Find("aim").transform.localPosition = m_Hit.point;
				Debug.Log(m_Hit.point);
				firebullet();
				//}
				
				}
			}
		}
	}


	void firebullet()
	{
		Instantiate(bullet,new Vector3(2,3,0), Quaternion.identity);
		Instantiate(bullet,new Vector3(-2,3,0), Quaternion.identity);
	}


	
}
