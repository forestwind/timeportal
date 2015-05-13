using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class GameUI : MonoBehaviour {
	
	public GUISkin GuiSkin;
	private Rect WindowRect;
	private bool isquest = true;
	private int CurrentStage;
	public bool isclear=false;
	public bool isfail=false;
	private float time;

	public AudioClip StageFail;

	// Use this for initialization
	void Start () {
		CurrentStage = GameObject.Find("GameManager").GetComponent<GameManager>().CurrentStage;

	}
	
	// Update is called once per frame
	void Update () {

		time=GameObject.Find("GameManager").GetComponent<GameManager>().GameTime;


		if (CurrentStage == 0) {

			if (time <= 42) {
				isquest = false;
			}
			if (time <= 0 && isfail == false) {
				isfail = true;

				GameObject.Find ("FreeLookCameraRig").GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().clip = StageFail;
				GetComponent<AudioSource> ().PlayDelayed (1.0f);
			}
			if (time <= 10) {
				if (GetComponent<AudioSource> ().isPlaying == false)
					GetComponent<AudioSource> ().Play ();
			}

		} else if (CurrentStage == 1) {
			if (time <= 0 && isfail == false) {
				isfail = true;
				GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().clip = StageFail;
				GetComponent<AudioSource> ().PlayDelayed (1.0f);

			}
		} else if (CurrentStage == 2) {

			if (time <= 97) {
				isquest = false;
			}
			if(time <= 0)
				isfail = true;
		}

	}
	
	void OnGUI () {

		GUI.skin = GuiSkin;

		if(isquest==true)
			WindowRect = GUI.Window (0, new Rect(Screen.width * 1/4, Screen.height * 1/5 ,Screen.width / 2 , Screen.height / 7 ), Quest, "Quest");

		if(isclear==true){
			WindowRect = GUI.Window (1, new Rect(Screen.width * 1/4, Screen.height * 1/5 ,Screen.width / 2 , Screen.height / 7 ), Clear, "Clear");
		}

		if(isfail==true) {
			WindowRect = GUI.Window (2, new Rect(Screen.width * 1/4, Screen.height * 1/5 ,Screen.width / 2 , Screen.height / 7 ), Fail, "Fail");
		}

		if (CurrentStage == 2) {

			GUI.Label(new Rect(Screen.width * 8/10, Screen.height * 1/7 ,Screen.width / 2 , Screen.height / 7 ),"Score : " + 
			          GameObject.Find("GameManager").GetComponent<GameManager>().Score);
			if(GameObject.Find("GameManager").GetComponent<GameManager>().Score > 500) {
				isclear=true;
			}

		}

	}
	
	void Quest( int windowID )
	{

		if(CurrentStage == 0){

			GUI.Label(new Rect(WindowRect.x * 2/4, WindowRect.y * 1/5 ,WindowRect.x , WindowRect.y ),"제한된 시간까지 조종석까지 이동하라");
		
		}

		else if(CurrentStage == 1){
			GUI.Label(new Rect(WindowRect.x * 2/4, WindowRect.y * 1/5 ,WindowRect.x , WindowRect.y ),"레이저를 쏴서 보급품을 얻어오시오");

			if(GUI.Button(new Rect(WindowRect.x * 3/4, WindowRect.y * 2/5 ,WindowRect.x * 2/4 , WindowRect.y/5),"Start")) {
				isquest=false;
				GameObject.Find("GameManager").GetComponent<GameManager>().isStageStart = true;
				GetComponent<AudioSource>().Play();
			}
		}

		else if(CurrentStage == 2){
			GUI.Label(new Rect(WindowRect.x * 2/4, WindowRect.y * 1/5 ,WindowRect.x , WindowRect.y ),"날아오는 적들을 없애시오");
			
//			if(GUI.Button(new Rect(WindowRect.x * 3/4, WindowRect.y * 2/5 ,WindowRect.x * 2/4 , WindowRect.y/5),"Start")) {
//				isquest=false;
//			}
		}

	}

	void Clear( int windowID )
	{
		if (CurrentStage == 0) {
			GUI.Label (new Rect (WindowRect.x * 2 / 4, WindowRect.y * 1 / 5, WindowRect.x, WindowRect.y), " 0Stage를 통과하셨습니다.");
		} else if (CurrentStage == 1)
			GUI.Label (new Rect (WindowRect.x * 2 / 4, WindowRect.y * 1 / 5, WindowRect.x, WindowRect.y), " 우측으로 이동해 다음 지령을 수행하시오.");
		else if (CurrentStage == 2 ) {
			GUI.Label (new Rect (WindowRect.x * 2 / 4, WindowRect.y * 1 / 5, WindowRect.x, WindowRect.y), " Game Clear! ");

			if(GUI.Button(new Rect(WindowRect.x * 3/4, WindowRect.y * 2/5 ,WindowRect.x * 2/4 , WindowRect.y/5),"MainMenu")) {
				Application.LoadLevel("MainMenu");
			}
		}
	}


	void Fail( int windowID )
	{


		if(CurrentStage==0){



			GUI.Label(new Rect(WindowRect.x * 3/5, WindowRect.y * 1/5 ,WindowRect.x , WindowRect.y )," 0 Stage를 통과하지 못했습니다.");

			if(GUI.Button(new Rect(WindowRect.x * 3/4, WindowRect.y * 2/5 ,WindowRect.x * 2/4 , WindowRect.y/5),"MainMenu")) {
				Application.LoadLevel("MainMenu");
			}
		}
		else if(CurrentStage==1){

		


			GUI.Label(new Rect(WindowRect.x * 3/5, WindowRect.y * 1/5 ,WindowRect.x , WindowRect.y )," 1 Stage를 통과하지 못했습니다.");
			
			if(GUI.Button(new Rect(WindowRect.x * 3/4, WindowRect.y * 2/5 ,WindowRect.x * 2/4 , WindowRect.y/5),"MainMenu")) {
				Application.LoadLevel("MainMenu");
			}
		}
	}
}
