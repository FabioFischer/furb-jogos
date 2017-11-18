using UnityEngine;

public class TextCtrl : MonoBehaviour 
{
	public GameObject textBox;
	public GameObject okButton;

	// Hide text box and ok button  
	public void HideTextBox()
	{
		textBox.SetActive(false);
		okButton.SetActive(false);
	}

}
