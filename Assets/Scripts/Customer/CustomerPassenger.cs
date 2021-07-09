using System;
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
        public bool hasJumped;
        [SerializeField] private Collider magnet;
        [SerializeField] private Collider collider;
        [SerializeField] private GameObject speechBubble;

        private void OnEnable()
        {
            _gameManager = FindObjectOfType<GameManager>();
            customerManager = FindObjectOfType<CustomerManagerPayment>();
            StartCoroutine(Despawn());
            
            if (hasJumped)
            {
                speechBubble.SetActive(false);
            }
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return; 
            
            if (customerManager.areSeatsFull) return;
            
            SpawnHand();
        }

        private void SpawnHand()
        {
            _gameManager.AddPassenger(gameObject);
            
            customerManager.Spawn();

            gameObject.SetActive(false);
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

            if (hasJumped)
            {
                _isBoarding = false;
                magnet.enabled = false;
                collider.enabled = false;
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