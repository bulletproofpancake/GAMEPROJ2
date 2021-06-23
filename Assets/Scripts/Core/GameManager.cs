using System.Collections.Generic;
using Customer;
using Stations;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public int stationPaymentMin;
        public int customerPaymentCap;
        public float levelDuration;
        //public List<StationData> stations;
        //public bool isJeepActive;
        public bool hasGameStarted;

        public List<GameObject> passengersList;

        public TimelineManager timelineManager;

        private void Awake()
        {
            passengersList = new List<GameObject>();
        }

        private void Start()
        {
            timelineManager = FindObjectOfType<TimelineManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Only triggers when the player first moves
                if (!hasGameStarted)
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
            SceneLoader.Instance.LoadNextScene();
            print("Game Over");
        }

        public void AddPassenger(GameObject customer)
        {
            passengersList.Add(customer);
        }

        public void RemovePassenger(GameObject customer)
        {
            passengersList.Remove(customer);
        }
    
    }
}
