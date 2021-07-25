using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

     [SerializeField]
    private GameObject thing;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float animationDuration;

    [SerializeField] private GameObject difficultyMenuCanvas;

    [SerializeField] private Button startButton;
    
    private void Awake()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.StopAll();
    }

    void Start() 
    {
       SceneLoader.Instance.Play("BlackToFade");
       //AudioManager.Instance.Play("BGM_Faded");
       AudioManager.Instance.Play("StartIdle");
       difficultyMenuCanvas.SetActive(false);
       anim = thing.GetComponent<Animator>();
    }


    public void OpenDifficultyMenu()
    {
        if(SceneLoader.Instance.isTutorialDone)
        {
            AudioManager.Instance.Play("Click");
            difficultyMenuCanvas.SetActive(true);
        }
        else
        {
            Load();
        }
    }
    
    public void Load()
    {
        AudioManager.Instance.Play("Click");
        anim.SetBool("isStart", false);
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        startButton.interactable = !startButton.interactable; //make start button not repeatable press
        difficultyMenuCanvas.SetActive(false);
        AudioManager.Instance.Play("Ignition");
        yield return new WaitForSeconds(1.25f);
        //Lahat ng animations mo ilagay mo dito
        anim.SetBool("isStart", true);

        //Ilagay mo dun sa animation duration yung haba nung animation na gagawin mo para saka lang magloload kapag tapos na yung animation
        yield return new WaitForSeconds(animationDuration * 0.5f);
        SceneLoader.Instance.Play("FadeToBlack");
        yield return new WaitForSeconds(1f);
        //Comment out mo to pag meron ka nang duration
        //yield return null;
        //AudioManager.Instance.Stop("StartIdle");
        //AudioManager.Instance.Fade("Ignition");
        //AudioManager.Instance.Stop("Ignition");
        //AudioManager.Instance.StopAll();
        SceneLoader.Instance.LoadGame();
    }
}