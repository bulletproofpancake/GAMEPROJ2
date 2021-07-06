using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject overlay;
    [SerializeField] private TextMeshProUGUI titleDisplay, bodyDisplay;
    [SerializeField] private Image imageDisplay;
    [SerializeField] private TutorialInfo[] tutorialInfos;
    [SerializeField] private int _tutorialIndex;
    [SerializeField] private TutorialInfo info;
    private GameManager _gameManager;
    public bool fromSpecific;
    public bool isObstacleTutorialDone;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void ShowTutorial()
    {
        _gameManager.pause.TogglePause(false);
        overlay.SetActive(true);
        info = tutorialInfos[_tutorialIndex];
        titleDisplay.text = info.Title;
        bodyDisplay.text = info.Body;
        imageDisplay.sprite = info.Display;
    }
    
    public void ShowTutorial(TutorialInfo tutorialInfo)
    {
        _gameManager.pause.TogglePause(false);
        overlay.SetActive(true);
        titleDisplay.text = tutorialInfo.Title;
        bodyDisplay.text = tutorialInfo.Body;
        imageDisplay.sprite = tutorialInfo.Display;
    }

    public void Continue()
    {
        if(!fromSpecific)
        {
            if (_tutorialIndex < tutorialInfos.Length - 1)
            {
                _tutorialIndex++;
            }
            else
            {
                _gameManager.hasCompletedTutorial = true;
                SceneLoader.Instance.isTutorialDone = true;
            }
        }

        _gameManager.pause.TogglePause(false);
        //_gameManager.hasGameStarted = true;
        overlay.SetActive(false); 
    }
    
    
}
