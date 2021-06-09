using System;
using UnityEngine;

namespace Customer
{
    public class CustomerPassenger : MonoBehaviour
    {
        [HideInInspector] public CustomerManagerPayment customerManager;

        private void Start()
        {
            customerManager = FindObjectOfType<CustomerManagerPayment>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return; 
            
            if (customerManager.areSeatsFull) return;
            
            customerManager.Spawn();
            Destroy(gameObject);
        }
    }
}