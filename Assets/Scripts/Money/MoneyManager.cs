using System;
using System.Collections;
using System.Collections.Generic;
using Customer;
using TMPro;
using UnityEngine;

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        private float _currentTotal;
        private List<GameObject> _moneyPrefabs;
        private Vector3 _startingPosition;
        
        public CustomerHand customer;
        public float paymentReceived;

        public float stationCost;

        private void Awake()
        {
            _moneyPrefabs = new List<GameObject>();
        }

        private void Start()
        {
            _startingPosition = transform.position;
            moneyDisplay.text = string.Empty;
        }

        private void Update()
        {
            if (customer != null)
                moneyDisplay.text = $"{paymentReceived} - {stationCost} = {_currentTotal}";
        }
        
        private void ReturnToStartingPosition()
        {
            transform.position = _startingPosition;
        }
        
        public void AddMoney(float moneyToAdd,GameObject moneyObject)
        {
            _currentTotal += moneyToAdd;
            _moneyPrefabs.Add(moneyObject);
        }
        
        public void GiveMoney()
        {
            if (customer == null)
            {
                Debug.LogWarning("No Customer Found");
                return;
            }

            if (_currentTotal == 0)
            {
                Debug.LogWarning("No money to give to customer");
                customer = null;
                return;
            }
            
            print($"Gave {_currentTotal} to passenger");
            
            if (_currentTotal == customer.MoneyToReceive)
            {
                Debug.LogWarning("Correct");
                customer.ReceivePayment(true);
            }
            else
            {
                Debug.LogWarning("Wrong");
                customer.ReceivePayment(false);
            }
            
            ClearMoney();

            //After giving the money to the customer
            //The money manager removes references to that customer
            customer = null;
        }

        public void ClearMoney()
        {
            moneyDisplay.text = string.Empty;
            print($"Cleared {_currentTotal} pesos");
            _currentTotal = 0;
            foreach (var moneyPrefab in _moneyPrefabs)
            {
                //TODO: SET TO DISABLE WHEN OBJECT POOL IS MADE
                Destroy(moneyPrefab);
            }
            ReturnToStartingPosition();
        }

        private void OnMouseUp()
        {
            ReturnToStartingPosition();
        }
    }
}
