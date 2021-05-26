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

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            timelineDisplay.maxValue = _gameManager.levelDuration;
        }

        private void Update()
        {
            timelineDisplay.value += Time.deltaTime;
        }
    }
}