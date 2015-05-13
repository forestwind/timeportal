using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class DemoSceneManager : MonoBehaviour
{
    public SmoothFollowCSharp CameraFollowCSharp;
    public Camera MissileCamera;
    public DemoRandomPositionMover3D targetMoverScriptRef;
    public int maxMissiles = 10;
    public List<GameObject> missileColletion = new List<GameObject>(); 
    public GameObject missilePrefab;
    public float missileSpeedSliderValue = 0.35f;
    public float missileProportionalConstSliderValue = 0.55f;
    public float targetChangeSpeed = 1.5f;
    public GUIText propConstLabel, maxSpeedLabel, activeMissiles;

    private float gameSpeed = 1.0f;
    private bool _missileCameraEnabled;

    public bool MissileCameraEnabled
    {
        get { return _missileCameraEnabled; }
        set
        {
            _missileCameraEnabled = value;
            if (MissileCamera != null) MissileCamera.enabled = _missileCameraEnabled;
        }
    }

    // Use this for initialization
    void Start ()
    {
        MissileCameraEnabled = true;
    }

    void OnGUI()
    {
        missileSpeedSliderValue = GUI.HorizontalSlider(new Rect(25, 40, 200, 20), missileSpeedSliderValue, 0.05F, 1.5F);
		missileProportionalConstSliderValue = GUI.HorizontalSlider(new Rect(25, 90, 200, 20), missileProportionalConstSliderValue, 0.05F, 1.0F);
        //gameSpeed = GUI.HorizontalSlider(new Rect(25, 140, 200, 20), gameSpeed, 0.00F, 1.0F);
        
        maxSpeedLabel.text = "New missile speed: " + Math.Round(missileSpeedSliderValue, 2).ToString();
        propConstLabel.text = "New missile proportional const: " + Math.Round(missileProportionalConstSliderValue, 2).ToString();

        if (GUI.Button(new Rect(25, 180, 140, 30), "Clear all missiles"))
        {
            // Get a handle to every current missile and destroy it through the missile component script.
            // In-efficient lookup for the foreach, but fine for use in a demo scene such as this one.
            foreach (var target in FindObjectsOfType<GameObject>().Where(t => t.name.Contains("HomingMissile3D")))
            {
                Debug.Log(target.name);
                var missileRef = target.GetComponent<MissileController3D>();
                missileRef.GracefullyDestroyMissile(target.transform.position);
            }
        }

        if (GUI.Button(new Rect(25, 220, 140, 30), "Toggle missile cam"))
        {
            MissileCameraEnabled = !MissileCameraEnabled;
        }

        activeMissiles.text = "Current active missiles: " + missileColletion.Count + "/10";
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Time.timeScale = gameSpeed;

        // Ensure the user is not clicking a GUI control - if not, then add the missile.
	    if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
	    {
            var targets = GameObject.FindGameObjectsWithTag("Targets").ToList();
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Note, you should ideally use Object Pooling rather than instantiation for mass missile use!
	        if (missileColletion.Count < maxMissiles)
	        {
                // We modify the position of instantiation here to ensure the Z-ordering is correct in the demo scene. Normally we would use the 2D layer sorting order for this, but there is an issue with Unity 4.3 where
                // exported assets do not retain layer sort ordering, and therefore we have to use Z depth for ordering here instead.
                var newMissile = (GameObject)Instantiate(missilePrefab, new Vector3(clickPosition.x, clickPosition.y, -3f), Quaternion.identity);

                // Adjust missile main performance properties based on scene slider values
                var missileScriptReference = newMissile.GetComponent<MissileController3D>();
                missileScriptReference.kProportionalConst = missileProportionalConstSliderValue;
                missileScriptReference.maxSpeed = missileSpeedSliderValue;

                // Target a random gameobject tagged with "Targets" found in the scene.
                missileScriptReference.target = targets[Random.Range(0, targets.Count - 1)];
                missileColletion.Add(newMissile);

                // Update missile cam to follow the newest missile added
                CameraFollowCSharp.target = newMissile.transform;
	        }
	    }

        // Clean up old missiles so we can fire more (depends on maxMissiles)
        for (var index = 0; index < missileColletion.Count; index++)
        {
            var missile = missileColletion[index];
            if (missile == null)
            {
                missileColletion.RemoveAt(index);
            }
        }

	}
}
