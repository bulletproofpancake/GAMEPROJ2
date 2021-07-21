using System;
using System.Collections;
using System.Collections.Generic;
using Customer;
using Money;
using Stations;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private float stationCostMin;
        public int StationCost => (int) stationCostMin;
        [SerializeField] private float customerPaymentCap;
        public int CustomerPaymentCap => (int) customerPaymentCap;
        public float levelDuration;
        //public List<StationData> stations;
        //public bool isJeepActive;
        [HideInInspector] public bool hasGameStarted;

        public List<GameObject> passengersList;

        [HideInInspector] public TimelineManager timelineManager;

        [HideInInspector] public bool hasCompletedTutorial;

        [HideInInspector] public TutorialManager tutorialManager;
        [SerializeField] private GameObject gameOverDisplay;
        private TextMeshProUGUI gameOverText;
        public PauseGame pause;
        [SerializeField] private TutorialInfo completeTutorial;
        [SerializeField] private TutorialInfo moneyFail;

        public MoneyManager moneyManager;
        public bool isGameOver;
        private void Awake()
        {
            passengersList = new List<GameObject>();

        }

        private void Start()
        {
            isGameOver = false;
            SceneLoader.Instance.Play("BlackToFade");
            gameOverDisplay.SetActive(false);
            gameOverText = gameOverDisplay.GetComponentInChildren<TextMeshProUGUI>();
            timelineManager = FindObjectOfType<TimelineManager>();
            tutorialManager = FindObjectOfType<TutorialManager>();
            moneyManager = FindObjectOfType<MoneyManager>();
            pause = FindObjectOfType<PauseGame>();
            if (tutorialManager == null) hasCompletedTutorial = true;
            else
            {
                Invoke("CallTutorial",1f);
            }
        }

        private void Update()
        {

            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     //Only triggers when the player first moves
            //     if (!hasGameStarted)
            //     {
            //         hasGameStarted = true;
            //         timelineManager.StartCountDown();
            //     }
            // }

            if (hasGameStarted)
            {
                switch (DifficultyManager.Instance.Difficulty)
                {
                    case DifficultySelection.Easy:
                        customerPaymentCap += Time.deltaTime / 7f;
                        stationCostMin += Time.deltaTime / 5f;
                        break;
                    case DifficultySelection.Medium:
                        customerPaymentCap += Time.deltaTime / 5f;
                        stationCostMin += Time.deltaTime / 3f;
                        break;
                    case DifficultySelection.Hard:
                        customerPaymentCap += Time.deltaTime / 3f;
                        stationCostMin += Time.deltaTime;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    hasGameStarted = true;
                    AudioManager.Instance.Play("Gas");
                    timelineManager.StartCountDown();
                }
            }
        }

        // public StationData RandomizeStation()
        // {
        //     var station = stations[Random.Range(0,stations.Count)];
        //     return station;
        // }
        //
        // public void RemoveStation()
        // {
        //     //FIX MONEY COSTS WHEN REMOVING STATIONS
        //     if (stations.Count == 1)
        //     {
        //         Debug.LogWarning("Game Over");
        //         return;
        //     }
        //
        //     stations.Remove(stations[0]);
        //     print(stations[0]);
        // }

        public void GameOver()
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
            print("Game Over");
        }

        IEnumerator GameOverSequence()
        {
            if (tutorialManager != null)
            {
                if(!moneyManager.hitFloor)
                {
                    CallTutorial(completeTutorial);
                    gameOverText.text = "Time's Up!";
                }
                else
                {
                    CallTutorial(moneyFail);
                    gameOverText.text = "Game Over!";
                }
                
                yield return new WaitForSeconds(1f);
            }
            
            gameOverText.text = !moneyManager.hitFloor ? "Time's Up!" : "Game Over!";
            
            RoundStatManager.Instance.Earn();
            gameOverDisplay.SetActive(true);
            pause.TogglePause(false);
            yield return new WaitForSecondsRealtime(2f);
            pause.TogglePause(false);
            SceneLoader.Instance.Play("FadeToBlack");
            yield return new WaitForSeconds(1f);
            SceneLoader.Instance.LoadScene("EndScreen");
        }
        public void AddPassenger(GameObject customer)
        {
            passengersList.Add(customer);
        }

        public void RemovePassenger(GameObject customer)
        {
            passengersList.Remove(customer);
        }

        public void CallTutorial()
        {
            //hasGameStarted = false;
            if (hasCompletedTutorial) return;
            tutorialManager.fromSpecific = false;
            tutorialManager.ShowTutorial();
        }
        
        public void CallTutorial(TutorialInfo tutorialInfo)
        {
            //hasGameStarted = false;
            //if (hasCompletedTutorial) return;
            tutorialManager.fromSpecific = true;
            tutorialManager.ShowTutorial(tutorialInfo);
        }

    }
}
