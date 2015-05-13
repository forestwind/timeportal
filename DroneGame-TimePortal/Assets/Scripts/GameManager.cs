using UnityEngine;

using System;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;

public class GameManager : MonoBehaviour {
	
	public int CurrentStage;

	public GameObject Enemy_1;
	public GameObject Enemy_2;
	public AudioClip PortalOpen;
	
	NyARUnityMarkerSystem _ms;

	
	public float Enemy1CreateTime = 2.0f;
	public float Enemy2CreateTime = 5.0f;
	
	public int maxEnemy_1 = 5;
	public int maxEnemy_2 = 3;
	
	public bool isGameOver = false;
	public bool isStageStart = false;

	public int Score = 0;
	
	int enemy1Count = 0;
	int enemy2Count = 0;
	
	AudioSource audio;
	
	float _time1 = 0.0f;
	float _time2 = 0.0f;
	
	bool isAppearance = false;
	
	public float GameTime;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();

		if(CurrentStage == 0){
			GameTime = 45.0f;
		}
		else if(CurrentStage == 1){
			GameTime = 60.0f;
		}
		else if(CurrentStage == 2){
			GameTime = 100.0f;
		}

		if(CurrentStage == 1 || CurrentStage == 2 ) {
			_ms = GameObject.Find("DroneCamera").GetComponent<DroneController>()._ms;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(CurrentStage == 0){
			GameTime -= Time.deltaTime;
		}
		else if(CurrentStage == 1 && isStageStart == true ){
			GameTime -= Time.deltaTime;
		}
		else if(CurrentStage == 2 ){
			GameTime -= Time.deltaTime;
		}


		if(CurrentStage == 2) {

			if (this._ms.isExistMarker (1)) {
				_time1 += Time.deltaTime;
				_time2 += Time.deltaTime;
				if(isAppearance==false)
				{
					audio.clip = PortalOpen;
					audio.Play();
					isAppearance = true;
				}
				
				if(enemy1Count < maxEnemy_1){
					if(_time1 > Enemy1CreateTime){
						float x = UnityEngine.Random.Range (-10f, 10f);
						float y = UnityEngine.Random.Range (-10f, 10f);
						
						Vector3 pos = GameObject.Find ("MarkerObject1").transform.localPosition;
						
						Instantiate (Enemy_1, new Vector3 (pos.x - x, pos.y - y, pos.z), Quaternion.identity);
						enemy1Count++;
						_time1 = 0.0f;
					}
				}
				
				if(enemy2Count < maxEnemy_2)
				{
					if(_time2 > Enemy2CreateTime)
					{
						float x = UnityEngine.Random.Range (-20f, 20f);
						float y = UnityEngine.Random.Range (-20f, 20f);
						
						Vector3 pos = GameObject.Find ("MarkerObject1").transform.localPosition;
						
						Instantiate (Enemy_2, new Vector3 (pos.x - x, pos.y - y, pos.z), Enemy_2.GetComponent<Transform>().rotation);
						enemy2Count++;
						_time2 = 0.0f;
					}
				}
				Debug.Log(Score);
			}
		}
	}
	
	
}