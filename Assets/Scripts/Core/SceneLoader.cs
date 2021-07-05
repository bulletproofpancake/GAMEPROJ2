using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public bool isTutorialDone;
    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        if (isTutorialDone)
        {
            SceneManager.LoadScene("GameSceneRework");
        }
        else
        {
            SceneManager.LoadScene("Tutorial Scene");
        }
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
}
