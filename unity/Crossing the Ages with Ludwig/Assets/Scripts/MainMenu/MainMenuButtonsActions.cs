using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsActions : MonoBehaviour 
{
	public GameObject textBox;
	public GameObject buttonBack;

	/// <summary>
	/// On button Play click, load prologue scene.
	/// To a scene be loaded, it has to be added on Edit->Build and Settings.
	/// </summary>
	public void Play()
	{
		SceneManager.LoadScene("Prologue");
	}

	/// <summary>
	/// Show details about the game.
	/// </summary>
	public void About()
	{
		buttonBack.SetActive(true);		
		textBox.SetActive(true);
	}

	public void HideAboutBox()
	{
		textBox.SetActive(false);
		buttonBack.SetActive(false);
	}


	/// <summary>
	/// Exit game on button click.
	/// </summary>	 	
	public void Exit()
	{
		Application.Quit();
	}
}
