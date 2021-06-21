using System.Collections.Generic;
using Customer;
using Stations;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public float levelDuration;
        public List<StationData> stations;
        public bool isJeepActive;

        private List<GameObject> _passengersList;

        private void Awake()
        {
            _passengersList = new List<GameObject>();
        }

        public StationData RandomizeStation()
        {
            var station = stations[Random.Range(0,stations.Count)];
            return station;
        }

        public void RemoveStation()
        {
            //FIXME: FIX MONEY COSTS WHEN REMOVING STATIONS
            if (stations.Count == 1)
            {
                Debug.LogWarning("Game Over");
                return;
            }
        
            stations.Remove(stations[0]);
            print(stations[0]);
        }

        public void GameOver()
        {
            //TODO: IMPLEMENT GAME OVER SEQUENCE
            SceneLoader.Instance.LoadNextScene();
            print("Game Over");
        }

        public void AddPassenger(GameObject customer)
        {
            _passengersList.Add(customer);
        }

        public void RemovePassenger(GameObject customer)
        {
            _passengersList.Remove(customer);
        }
    
    }
}
