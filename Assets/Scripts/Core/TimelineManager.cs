using System.Collections;
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
        [SerializeField] private Image fillImage;
        public bool hasReachedStation;
        public StationData stationReached;
        private Canvas _canvas;
        private float timer;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            display.maxValue = _gameManager.levelDuration;
            //fillImage.color = _gameManager.stations[0].Indicator;
            _canvas = GetComponent<Canvas>();
            //SetCamera();
            //StartCountDown();
        }
        
        // private void SetCamera()
        // {
        //     var cameras = FindObjectsOfType<Camera>();
        //
        //     foreach (var cam in cameras)
        //     {
        //         if (cam.CompareTag("2D Camera"))
        //         {
        //             _canvas.worldCamera = cam;
        //         }
        //     }
        // }

        private void Update()
        {
            display.value = timer;

            // hasReachedStation = display.value >= display.maxValue;
            //
            // if (!hasReachedStation) return;
            //
            // stationReached = _gameManager.stations[0];
            //
            // if (_gameManager.stations.Count == 1)
            // {
            //     _gameManager.GameOver();
            //     return;
            // }
            //     
            // _gameManager.RemoveStation();
            // display.value = 0f;
            // fillImage.color = _gameManager.stations[0].Indicator;
        }

        public void StartCountDown()
        {
            StartCoroutine(CountDown());
        }
        
        private IEnumerator CountDown()
        {
            while (timer < _gameManager.levelDuration)
            {
                //if (!_gameManager.hasGameStarted) continue;
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
            }
            _gameManager.GameOver();
        }
    }
}