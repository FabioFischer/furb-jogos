using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueCtrl : MonoBehaviour 
{
	public GameObject textBox;
	public GameObject okButton;

    
	/// <summary>
	/// On OkButton click, load fase1 scene.
	/// To a scene be loaded, it has to be added on Edit->Build and Settings.
	/// </summary>
	public void Ok()
	{
		// Find main menu music GameObject
		GameObject music = GameObject.FindGameObjectWithTag("Music");
		
		// Stop main menu music
		music.SetActive(false);

		// Load Fase1 scene
		SceneManager.LoadScene("Fase1");
	}
}
