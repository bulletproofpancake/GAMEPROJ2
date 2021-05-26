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

        public bool isGivingMoney;
        public float moneyToGive;
        public float moneyToReceive;
        public StationData stationSelected;

        private bool _hasReceivedPayment;

        private void Awake()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SelectStation();
            GivePayment();
        }

        private void SelectStation()
        {
            stationSelected = _gameManager.RandomizeStation();
            moneyToGive = stationSelected.Cost + Random.Range(1,11);
            moneyToReceive = moneyToGive - stationSelected.Cost;
        }

        private void GivePayment()
        {
            isGivingMoney = true;
            _moneyDisplay.text = moneyToGive.ToString();
        }

        private void Update()
        {
            if(!_hasReceivedPayment)
                //Changes the sprite color if this is selected by the money manager
                _spriteRenderer.color = _moneyManager.customer == this ? Color.yellow : Color.white;
        }

        private void OnMouseDown()
        {
            if (isGivingMoney)
            {
                _moneyManager.paymentReceived = moneyToGive;
                _moneyManager.stationCost = stationSelected.Cost;
                _moneyDisplay.text = string.Empty;
                isGivingMoney = false;
            }
            
            _moneyManager.customer = this;
            
            print(isGivingMoney);
        }

        IEnumerator Respond(bool isCorrect)
        {
            _spriteRenderer.color = isCorrect ? Color.green : Color.red;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        
        public void ReceivePayment(bool isCorrect)
        {
            _hasReceivedPayment = true;
            StartCoroutine(Respond(isCorrect));
        }

    }
}
