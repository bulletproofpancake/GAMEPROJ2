using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private Animator animator;
    public bool isTutorialDone;
    public void LoadNextScene()
    {
        Play("FadeToBlack");
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        //Play("FadeToBlack");
        AudioManager.Instance.Play("Click");
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
        //Play("FadeToBlack");
        SceneManager.LoadScene(sceneName);
    }
    
    public void Exit()
    {
        AudioManager.Instance.Play("Click");
        Application.Quit();
    }

    public void Play(string animationName)
    {
        animator.Play(animationName);
    }

}
