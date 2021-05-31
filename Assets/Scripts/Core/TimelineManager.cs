using System;
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
        private bool _hasReachedStation;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            display.maxValue = _gameManager.levelDuration;
            handle.color = _gameManager.stations[0].Indicator;
        }

        private void Update()
        {
            display.value += Time.deltaTime;

            _hasReachedStation = display.value >= display.maxValue;

            if (!_hasReachedStation) return;
            
            if (_gameManager.stations.Count == 1)
            {
                //TODO: CALL GAME OVER FUNCTION HERE
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