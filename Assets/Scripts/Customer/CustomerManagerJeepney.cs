using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerManagerJeepney : MonoBehaviour
    {
        [SerializeField] private GameObject[] passengers;
        [SerializeField] private float timeToSpawnCap;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var passenger = Instantiate(passengers[Random.Range(0, passengers.Length)],transform.parent);
        }
        
    }
}