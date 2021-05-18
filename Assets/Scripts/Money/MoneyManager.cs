using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        private float _currentTotal;
        private List<GameObject> _moneyPrefabs;

        private void Awake()
        {
            _moneyPrefabs = new List<GameObject>();
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
            print($"Gave {_currentTotal} to passenger");
            ClearMoney();
        }

        public void ClearMoney()
        {
            _currentTotal = 0;
            foreach (var moneyPrefab in _moneyPrefabs)
            {
                Destroy(moneyPrefab);
            }
        }
    }
}
