using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneyData data;

        public void SpawnMoney()
        {
            Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
        }
    }

}