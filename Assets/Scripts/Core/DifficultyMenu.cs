using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{
    private StartGame _startGame;

    private void Start()
    {
        _startGame = FindObjectOfType<StartGame>();
    }

    public void SetDifficulty(int difficulty)
    {
        DifficultyManager.Instance.SetDifficulty(difficulty);
        _startGame.Load();
    }
    
}
