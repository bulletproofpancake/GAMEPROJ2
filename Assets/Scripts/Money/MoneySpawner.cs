using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        private MoneyManager _moneyManager;

        private void Start()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
        }

        public void SpawnMoney()
        {
            //TODO: SWITCH TO AN OBJECT POOL
            var money = Instantiate(data.Prefab, _moneyManager.transform);
            _moneyManager.AddMoney(data.Value, money);
        }
    }

}