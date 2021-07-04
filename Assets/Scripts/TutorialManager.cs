using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private TextMeshProUGUI titleDisplay, bodyDisplay;
    [SerializeField] private Image imageDisplay;
    [SerializeField] private TutorialInfo[] tutorialInfos;
    private int _tutorialIndex;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        var info = tutorialInfos[_tutorialIndex];
        titleDisplay.text = info.Title;
        bodyDisplay.text = info.Body;
        imageDisplay.sprite = info.Display;
    }

    public void Continue()
    {
        if(_gameManager.hasCompletedTutorial)
            overlay.SetActive(false);
        else
        {
            if(_tutorialIndex< tutorialInfos.Length)
                _tutorialIndex++;
            else
            {
                _gameManager.hasCompletedTutorial = true;
            }

            overlay.SetActive(false);
        }

    }
    
}
