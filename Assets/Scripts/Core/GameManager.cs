using System;
using System.Collections;
using System.Collections.Generic;
using Customer;
using Stations;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        private float stationCostMin;
        public int StationCost => (int) stationCostMin;
        private float customerPaymentCap;
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
        public PauseGame pause;
        [SerializeField] private TutorialInfo completeTutorial;

        private void Awake()
        {
            passengersList = new List<GameObject>();

        }

        private void Start()
        {
            SceneLoader.Instance.Play("BlackToFade");
            gameOverDisplay.SetActive(false);
            timelineManager = FindObjectOfType<TimelineManager>();
            tutorialManager = FindObjectOfType<TutorialManager>();
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
                        customerPaymentCap += Time.deltaTime / 3f;
                        stationCostMin += Time.deltaTime / 2f;
                        break;
                    case DifficultySelection.Medium:
                        customerPaymentCap += Time.deltaTime / 1.5f;
                        stationCostMin += Time.deltaTime / 0.5f;
                        break;
                    case DifficultySelection.Hard:
                        customerPaymentCap += Time.deltaTime * 1.5f;
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
        //     //FIXME: FIX MONEY COSTS WHEN REMOVING STATIONS
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
            StartCoroutine(GameOverSequence());
            print("Game Over");
        }

        IEnumerator GameOverSequence()
        {
            if (tutorialManager != null)
            {
                print("completed tutorial");
                CallTutorial(completeTutorial);
                yield return new WaitForSeconds(1f);
            }

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
            if (hasCompletedTutorial) return;
            tutorialManager.fromSpecific = true;
            tutorialManager.ShowTutorial(tutorialInfo);
        }

    }
}
