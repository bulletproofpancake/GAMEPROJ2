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
        private Vector3 startingPosition;
        
        public CustomerHand customer;

        private void Awake()
        {
            _moneyPrefabs = new List<GameObject>();
        }

        private void Start()
        {
            startingPosition = transform.position;
        }

        private void Update()
        {
            //if there's no money in the scene, UI is empty
            moneyDisplay.text = _currentTotal <= 0 ? string.Empty : $"{_currentTotal:0}";
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
            
            if (_currentTotal == customer.moneyToReceive)
            {
                Debug.LogWarning("Correct");
            }
            else
            {
                Debug.LogWarning("Wrong");
            }
            
            ClearMoney();
            
            
            
            //After giving the money to the customer
            //The money manager removes references to that customer
            customer = null;
        }

        public void ClearMoney()
        {
            print($"Cleared {_currentTotal} pesos");
            _currentTotal = 0;
            foreach (var moneyPrefab in _moneyPrefabs)
            {
                //TODO: SET TO DISABLE WHEN OBJECT POOL IS MADE
                Destroy(moneyPrefab);
            }
            ReturnToStartingPosition();
        }

        private void ReturnToStartingPosition()
        {
            transform.position = startingPosition;
        }

        private void OnMouseUp()
        {
            ReturnToStartingPosition();
        }
    }
}
