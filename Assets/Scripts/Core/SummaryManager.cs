using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryManager : Singleton<SummaryManager>
{
    [SerializeField] private TextMeshProUGUI moneyEarnedDisplay, obstaclesHitDisplay, totalEarningsDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.Play("BlackToFade");
        AudioManager.Instance.Play("Gas");
        AudioManager.Instance.Play("Ambiance");
        moneyEarnedDisplay.text = $"Money Earned: PHP {RoundStatManager.Instance.moneyEarned}";
        obstaclesHitDisplay.text = $"Obstacles Hit: {RoundStatManager.Instance.obstaclesHit} (-PHP {RoundStatManager.Instance.obstaclesHit*10})";
        totalEarningsDisplay.text = $"Total Earnings: PHP {RoundStatManager.Instance.net}";
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
