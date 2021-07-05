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

        private void Awake()
        {
            passengersList = new List<GameObject>();
        }

        private void Start()
        {
            gameOverDisplay.SetActive(false);
            timelineManager = FindObjectOfType<TimelineManager>();
            tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager == null) hasCompletedTutorial = true;
            else
            {
                CallTutorial();
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
                customerPaymentCap += Time.deltaTime / 3f;
                stationCostMin += Time.deltaTime / 2f;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    
                    hasGameStarted = true;
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
            //TODO: IMPLEMENT GAME OVER SEQUENCE
            StartCoroutine(GameOverSequence());
            print("Game Over");
            print($"{(int) stationCostMin}");
        }

        IEnumerator GameOverSequence()
        {
            RoundStatManager.Instance.Earn();
            gameOverDisplay.SetActive(true);
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(3f);
            Time.timeScale = 1f;
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
