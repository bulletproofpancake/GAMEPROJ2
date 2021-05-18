using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        [SerializeField] private MoneyManager moneyManager;

        private void Start()
        {
            moneyManager = FindObjectOfType<MoneyManager>();
        }

        public void SpawnMoney()
        {
            Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
            moneyManager.AddMoney(data.Value);
        }
    }

}