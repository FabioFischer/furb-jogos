using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnJump : MonoBehaviour 
{

	public AudioSource jumpSound;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			jumpSound.Play();
		}
	}
}
