using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TimelineManager : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private Slider[] timelineDisplay;

        private int _currentStation = 0;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();

            foreach (var slider in timelineDisplay)
            {
                slider.maxValue = _gameManager.levelDuration;
            }
            
        }

        private void Update()
        {
            DisplayTimeline();
        }

        void DisplayTimeline()
        {
            if (_currentStation >= timelineDisplay.Length) return;
            
            timelineDisplay[_currentStation].value += Time.deltaTime;
            
            if (timelineDisplay[_currentStation].value >= timelineDisplay[_currentStation].maxValue)
            {
                //TODO: IMPLEMENT CUSTOMER ARRIVAL AND REMOVAL AT STATIONS
                _currentStation++;
            }
        }
        
    }
}