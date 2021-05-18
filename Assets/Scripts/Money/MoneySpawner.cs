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
            Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
            _moneyManager.AddMoney(data.Value);
        }
    }

}