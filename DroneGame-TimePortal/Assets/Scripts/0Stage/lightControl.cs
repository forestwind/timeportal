using UnityEngine;
using System.Collections;

public class lightControl : MonoBehaviour {

	Color lightColor;
	int CurrentStage;
	float health;
	bool lightChange = false;
	// Use this for initialization
	void Start () {
		CurrentStage = GameObject.Find("GameManager").GetComponent<GameManager>().CurrentStage;
	}
	
	// Update is called once per frame
	void Update () {

		if(CurrentStage == 0){
		lightColor = GetComponent<Light>().color;
		lightColor.g -= 0.002f;
		lightColor.b -= 0.002f;
		GetComponent<Light>().color = lightColor;
		//Debug.Log(lightColor);
		}

		else if(CurrentStage == 1){
			health=GameObject.Find("DroneCamera").GetComponentInChildren<F3DBeam>().health;
			if( health < 50){

				if(lightChange==false){
					GetComponent<Light>().color=new Color(255,0,0);
					lightChange=true;
				}
				else if(lightChange==true){
					GetComponent<Light>().color=new Color(255,255,255);
					lightChange=false;
				}

			}
		}

	}
}
