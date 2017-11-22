using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAction : MonoBehaviour {
    // Restart Scene
   public void ResetGame()
    {
        SceneManager.LoadScene("Fase1");
    }
}
