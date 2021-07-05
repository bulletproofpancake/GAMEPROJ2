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
}
