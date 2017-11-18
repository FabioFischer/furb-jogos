using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KeepMusicPlaying : MonoBehaviour 
{
	private static KeepMusicPlaying instance = null;
    public static KeepMusicPlaying Instance { get { return instance; } }

	/// <summary>
	/// Use singleton pattern to keep music playing between scenes.
	/// </sumnary>
	void Awake()
	{
		if (instance != null && instance != this) 
		{
        	Destroy(this.gameObject);
            return;
        } else 
		{
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

		/* Keep music playing on a not beauty way. Change it to use singleton in the future!
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
		if(objs.Length > 1)
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject); */
	}
}
