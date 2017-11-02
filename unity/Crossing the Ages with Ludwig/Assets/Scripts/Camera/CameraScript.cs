using Assets.Scripts.Game;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    private Transform target;
    
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        this.target = GetTargetTransform();

        float screenVert = Camera.main.orthographicSize;
        float screenHoriz = screenVert * Screen.width / Screen.height;

        GameManager.GetCameraBoundaries
            (
                GameObject.Find("Quarto do ludwig").GetComponentInChildren<SpriteRenderer>(),
                screenHoriz,
                screenVert,
                out this.xMin, 
                out this.xMax, 
                out this.yMin, 
                out this.yMax            
            );
    }
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        this.target = GetTargetTransform();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void LateUpdate ()
    {
        transform.position = new Vector3
            (
                Mathf.Clamp(target.position.x, xMin, xMax), 
                Mathf.Clamp(target.position.y, yMin, yMax), 
                transform.position.z
            );
    }

    private Transform GetTargetTransform()
    {
        return GameObject.Find(Sofie.GetResourceName).transform;
    }
}
