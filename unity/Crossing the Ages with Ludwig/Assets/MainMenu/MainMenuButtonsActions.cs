using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsActions : MonoBehaviour 
{
	/// <summary>
	/// On button Play click, load Fase1 scene.
	/// To a scene be loaded, it has to be added on Edit->Build and Settings.
	/// </summary>
	public void Play()
	{
		SceneManager.LoadScene("Fase1");
	}

	/// <summary>
	/// Show details about the game.
	/// </summary>
	public void About()
	{

	}

	/// <summary>
	/// Exit game on button click.
	/// </summary>	 	
	public void Exit()
	{
		Application.Quit();
	}
}
