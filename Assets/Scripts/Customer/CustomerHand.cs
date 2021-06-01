using System;
using System.Collections;
using Core;
using Money;
using Stations;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {

        private GameManager _gameManager;
        private MoneyManager _moneyManager;
        
        private TextMeshPro _moneyDisplay;
        private SpriteRenderer _spriteRenderer;

        public int paymentCap;
        
        private float _moneyToGive;
        
        public float MoneyToReceive { get; private set; }
        
        private StationData _stationSelected;

        private bool _hasReceivedPayment;
        
        public CustomerSpawner Spawner { get; set; }
        public int SeatTaken { get; set; }
        
        public float TimeSpawned { get; set; }

        private void Awake()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            SelectStation();
            GivePayment();
        }

        private void OnDisable()
        {
            Spawner.seat[SeatTaken].isTaken = false;
        }

        private void SelectStation()
        {
            _stationSelected = _gameManager.RandomizeStation();
            _moneyToGive = _stationSelected.Cost + Random.Range(1,paymentCap);
            MoneyToReceive = _moneyToGive - _stationSelected.Cost;
            print($"{_stationSelected}");
        }

        private void GivePayment()
        {
            _moneyDisplay.text = _moneyToGive.ToString();
        }

        private void Update()
        {
            if(!_hasReceivedPayment)
                //Changes the sprite color if this is selected by the money manager
                _spriteRenderer.color = _moneyManager.customer == this ? Color.yellow : Color.white;
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
            Destroy(gameObject);
        }
        
        public void ReceivePayment(bool isCorrect)
        {
            _hasReceivedPayment = true;
            StartCoroutine(Respond(isCorrect));
        }

    }
}
