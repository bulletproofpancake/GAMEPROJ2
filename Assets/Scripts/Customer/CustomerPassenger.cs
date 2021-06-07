using System;
using UnityEngine;

namespace Customer
{
    public class CustomerPassenger : MonoBehaviour
    {
        [HideInInspector] public CustomerManager customerManager;

        private void Start()
        {
            customerManager = FindObjectOfType<CustomerManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            customerManager.Spawn();
            Destroy(gameObject);
        }
    }
}