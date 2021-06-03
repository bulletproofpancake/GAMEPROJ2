using Stations;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TimelineManager : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private Slider display;
        public Slider Display => display;
        [SerializeField] private Image handle;
        public bool hasReachedStation;
        public StationData stationReached;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            display.maxValue = _gameManager.levelDuration;
            handle.color = _gameManager.stations[0].Indicator;
        }

        private void Update()
        {
            display.value += Time.deltaTime;

            hasReachedStation = display.value >= display.maxValue;

            if (!hasReachedStation) return;

            stationReached = _gameManager.stations[0];
            
            if (_gameManager.stations.Count == 1)
            {
                _gameManager.GameOver();
                return;
            }
                
            _gameManager.RemoveStation();
            display.value = 0f;
            handle.color = _gameManager.stations[0].Indicator;
        }

        public void SetMarker()
        {
            
        }
        
    }
}