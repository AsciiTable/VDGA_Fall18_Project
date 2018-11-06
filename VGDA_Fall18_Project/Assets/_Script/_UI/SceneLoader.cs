using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

//Load Scene with specific index
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

//Load Scene next in list (next level)
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

//Load Scene previous in list (previous part of level)
    public void PrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

//Leave Game Application
    public void LeaveGame()
    {
        Debug.Log("Leave Game");
        Application.Quit();
    }
}
