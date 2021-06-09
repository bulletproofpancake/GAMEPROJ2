using System.Collections;
using Core;
using Money;
using Stations;
using TMPro;
using UnityEngine;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {

        private GameManager _gameManager;
        private MoneyManager _moneyManager;
        
        private TextMeshPro _moneyDisplay;
        private SpriteRenderer _spriteRenderer;

        public int paymentCap;
        
        private int _moneyToGive;
        
        public int MoneyToReceive { get; private set; }
        
        private StationData _stationSelected;

        private bool _hasReceivedPayment;

        [HideInInspector] public CustomerManagerPayment customerManager;
        public int SeatTaken { get; set; }
        
        public float TimeSpawned { get; set; }

        private TimelineManager _timeline;
        

        private void Awake()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timeline = FindObjectOfType<TimelineManager>();
        }

        private void OnEnable()
        {
            SelectStation();
            GivePayment();
        }

        private void OnDisable()
        {
            customerManager.seats[SeatTaken].isTaken = false;
            customerManager.seatsTaken--;
        }

        private void SelectStation()
        {
            _stationSelected = _gameManager.RandomizeStation();
            _moneyToGive = _stationSelected.Cost + Random.Range(0, paymentCap + 1);
            MoneyToReceive = _moneyToGive - _stationSelected.Cost;
            print($"{_stationSelected}");
        }

        private void GivePayment()
        {
            _moneyDisplay.text = $"{_moneyToGive}";
        }

        private void Update()
        {
            if(_timeline.hasReachedStation && _timeline.stationReached == _stationSelected)
            {
                print($"{this} left");
                StartCoroutine(Leave());
            }
            
            if(!_hasReceivedPayment)
                //Changes the sprite color if this is selected by the money manager
                _spriteRenderer.color = _moneyManager.customer == this ? Color.white : _stationSelected.Indicator;
        }

        private void OnMouseDown()
        {
            _moneyManager.paymentReceived = _moneyToGive;
            _moneyManager.stationCost = _stationSelected.Cost;
            _moneyDisplay.text = string.Empty;

            _moneyManager.customer = this;
        }

        IEnumerator Respond(bool isCorrect)
        {
            _spriteRenderer.color = isCorrect ? Color.green : Color.red;
            yield return new WaitForSeconds(1f);
            if (!isCorrect)
            {
                _hasReceivedPayment = false;
                yield break;
            }
            //TODO: SWITCH TO DISABLE
            Destroy(gameObject);
        }

        IEnumerator Leave()
        {
            _hasReceivedPayment = true;
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(1f);
            _hasReceivedPayment = false;
            //TODO: SWITCH TO DISABLE
            Destroy(gameObject);
        }
        
        public void ReceivePayment(bool isCorrect)
        {
            _hasReceivedPayment = true;
            StartCoroutine(Respond(isCorrect));
        }

    }
}
