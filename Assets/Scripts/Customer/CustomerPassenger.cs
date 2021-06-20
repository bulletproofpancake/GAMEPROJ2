using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Customer
{
    public class CustomerPassenger : MonoBehaviour
    {
        [HideInInspector] public CustomerManagerPayment customerManager;
        private float _jumpSpeed = 0.05f;
        private bool _isBoarding;
        private GameObject jeepObject;

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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (customerManager.areSeatsFull) return;

            _isBoarding = true;

            jeepObject = other.gameObject;

        }

        private void Update()
        {
            if (_isBoarding)
            {
                transform.position = Vector3.Lerp(transform.position, jeepObject.transform.position, _jumpSpeed);
            }
        }
    }
}