﻿using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerPassenger : MonoBehaviour
    {
        [HideInInspector] public CustomerManagerPayment customerManager;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float lifespan = 10;
        private bool _isBoarding;
        private GameObject _jeepObject;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            customerManager = FindObjectOfType<CustomerManagerPayment>();
            StartCoroutine(Despawn());
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return; 
            
            if (customerManager.areSeatsFull) return;
            
            customerManager.Spawn();
            
            _gameManager.AddPassenger(gameObject);
            
            Destroy(gameObject);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (customerManager.areSeatsFull) return;

            _isBoarding = true;

            _jeepObject = other.gameObject;

        }

        private void Update()
        {
            if (_isBoarding)
            {
                transform.position = Vector3.Lerp(transform.position, _jeepObject.transform.position, jumpSpeed);
            }
        }

        private IEnumerator Despawn()
        {
            var timer = new float();
            while (timer < lifespan)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
        
    }
}