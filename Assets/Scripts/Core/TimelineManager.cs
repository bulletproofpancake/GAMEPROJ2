using System;
using Stations;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TimelineManager : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private Slider timelineDisplay;
        [SerializeField] private Image handle;
        private bool _hasReachedStation;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            timelineDisplay.maxValue = _gameManager.levelDuration;
        }

        private void Update()
        {
            handle.color = _gameManager.stations[0].Indicator;
            
            timelineDisplay.value += Time.deltaTime;

            _hasReachedStation = timelineDisplay.value >= timelineDisplay.maxValue;

            if (_hasReachedStation)
            {
                if (_gameManager.stations.Count == 1)
                {
                    return;
                }
                
                _gameManager.RemoveStation();
                timelineDisplay.value = 0f;
            }
            
        }
    }
}