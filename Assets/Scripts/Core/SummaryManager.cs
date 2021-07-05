using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryManager : Singleton<SummaryManager>
{
    [SerializeField] private TextMeshProUGUI moneyEarnedDisplay, obstaclesHitDisplay, totalPassengersDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyEarnedDisplay.text = $"Money Earned: {RoundStatManager.Instance.moneyEarned}";
        obstaclesHitDisplay.text = $"Obstacles Hit: {RoundStatManager.Instance.obstaclesHit}";
        totalPassengersDisplay.text = $"Total Passengers: {RoundStatManager.Instance.totalPassengers}";
    }

    public void LoadMainMenu()
    {
        print(RoundStatManager.Instance.totalMoney);
        RoundStatManager.Instance.EndRound();
        SceneLoader.Instance.LoadScene("Main Menu");
    }

    public void LoadGame()
    {
        print(RoundStatManager.Instance.totalMoney);
        RoundStatManager.Instance.EndRound();
        SceneLoader.Instance.LoadGame();
    }
    
}
