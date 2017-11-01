using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField]
    public static float xMax = 21.0f;

    [SerializeField]
    public static float yMax = 0.5f;

    [SerializeField]
    public static float xMin = -22.0f;

    [SerializeField]
    public static float yMin = 0f;

    private Transform target;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        target = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, xMin, xMax), 
            Mathf.Clamp(target.position.y, yMin, yMax), 
            transform.position.z);
    }
}
