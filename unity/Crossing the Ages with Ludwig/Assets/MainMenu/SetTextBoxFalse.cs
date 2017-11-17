using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextBoxFalse : MonoBehaviour {

	public GameObject textBox;
	public GameObject buttonBack;

	// Inicialize with "about" textbox disabled.
	void Start () 
	{
		textBox.SetActive(false);
		buttonBack.SetActive(false);
	}
}
