using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    [SerializeField] private DifficultySelection difficulty;
    public DifficultySelection Difficulty => difficulty;

    public void SetDifficulty(DifficultySelection selection)
    {
        difficulty = selection;
    }
    
}

public enum DifficultySelection
{
    Easy,
    Medium,
    Hard
}