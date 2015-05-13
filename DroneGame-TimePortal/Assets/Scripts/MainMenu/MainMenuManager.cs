using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	private Rect WindowRect;
	private bool isMenu = true;
	private bool isoption= false;
	private bool iscredit= false;

	public GUISkin GuiSkin;
	public Texture title;
	public AudioClip mouseClick;
	public Texture loading;
	public bool isloading=false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI () {
		GUI.skin = GuiSkin;

		if(isloading == false){
			if(isMenu == true){
				GUI.DrawTexture(new Rect((Screen.width * 1/5) ,Screen.height * 1/10 , Screen.width * 2/3 , Screen.height/4 ),title);
				WindowRect = GUI.Window (0, new Rect(Screen.width * 1/4, Screen.height * 1/2 ,Screen.width / 2 , Screen.height / 2 ), menu, "");
			}
			if(isoption == true){
				WindowRect = GUI.Window (1, new Rect(Screen.width * 1/4, Screen.height * 1/4 ,Screen.width / 2 , Screen.height / 2 ), option, "Option");
			}
			if(iscredit == true){
				WindowRect = GUI.Window (2, new Rect(Screen.width * 1/4, Screen.height * 1/4 ,Screen.width / 2 , Screen.height / 2 ), credit, "Credit");
			}
		}

		else if(isloading==true){
			GUI.DrawTexture(new Rect(0 ,0 , Screen.width, Screen.height ),loading);
		}
	}
	
	void menu( int windowID )
	{
		if(GUI.Button(new Rect( 0, 0 , WindowRect.x * 2 , WindowRect.y * 1/5 ),"Game Start")){
			Debug.Log ("Start");
			GetComponent<AudioSource>().PlayOneShot(mouseClick);
			isloading=true;
			Application.LoadLevelAsync("0Stage");


		}
		
		if(GUI.Button(new Rect( 0, WindowRect.y * 1/4 , WindowRect.x * 2 , WindowRect.y * 1/5  ),"Option")){
			Debug.Log ("Option");
			GetComponent<AudioSource>().PlayOneShot(mouseClick);
			isMenu=false;
			isoption=true;
		}
		
		if(GUI.Button(new Rect( 0, WindowRect.y * 2/4 , WindowRect.x * 2 , WindowRect.y * 1/5  ),"Credit")){
			Debug.Log ("Credit");
			GetComponent<AudioSource>().PlayOneShot(mouseClick);
			isMenu=false;
			iscredit=true;
		}
		
		if (GUI.Button (new Rect (0, WindowRect.y * 3 / 4, WindowRect.x * 2, WindowRect.y * 1 / 5), "Exit")) {
			Debug.Log ("Exit");
			Application.Quit();
		} 
	}

	void option( int windowID )
	{
		if(GUI.Button(new Rect( 0,  WindowRect.height * 4/5 , WindowRect.width  ,  WindowRect.height * 1/5 ),"Ok")){
			GetComponent<AudioSource>().PlayOneShot(mouseClick);
			isoption=false;
			isMenu=true;
		}

	}

	void credit (int windowID )
	{
		if(GUI.Button(new Rect( 0,  WindowRect.height * 4/5 , WindowRect.width  ,  WindowRect.height * 1/5 ),"Ok")){
			GetComponent<AudioSource>().PlayOneShot(mouseClick);
			iscredit=false;
			isMenu=true;
		}
	}


}
