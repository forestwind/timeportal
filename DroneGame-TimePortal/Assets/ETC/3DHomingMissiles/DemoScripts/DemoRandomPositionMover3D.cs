using UnityEngine;
using System.Collections;

public class DemoRandomPositionMover3D : MonoBehaviour {

    public float radius;

    public float pickerInterval;

    public GameObject centralPointObject;

    public Vector3 randomPointInCircle, originalStartPosition;
    private float _pickerInterval;

    // Use this for initialization
    void Start()
    {
        originalStartPosition = transform.position;

        if (pickerInterval == 0f)
        {
            pickerInterval = 1.5f;
        }

        randomPointInCircle = Vector3.zero;
        InvokeRepeating("PickRandomPointInCircle", Random.Range(0f, pickerInterval), pickerInterval);
    }

    private void PickRandomPointInCircle()
    {
        //transform.position = centralPointObject.transform.position;
        randomPointInCircle = originalStartPosition + Random.insideUnitSphere * radius;
        transform.position = randomPointInCircle;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
