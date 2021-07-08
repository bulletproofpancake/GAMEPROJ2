using System.Collections;
using System.Collections.Generic;
using Customer;
using UnityEngine;

namespace Core
{
    public class Obstacle : MonoBehaviour
    {
        private CustomerManagerPayment _customerManager;
        [SerializeField] private TutorialInfo tutorial;
        private GameManager _gameManager;

        private Rigidbody rb;
        private void Start()
        {
            _customerManager = FindObjectOfType<CustomerManagerPayment>();
            _gameManager = FindObjectOfType<GameManager>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            AudioManager.Instance.Play("Crash");
            if(_gameManager.tutorialManager != null)
            {
                if (!_gameManager.tutorialManager.isObstacleTutorialDone)
                {
                    _gameManager.CallTutorial(tutorial);
                    _gameManager.tutorialManager.isObstacleTutorialDone = true;

                }
            }
            print("ouch");
            if (_customerManager.customers.Count > 0)
            {
                _customerManager.customers[0].Leave();
            }
            // RigidbodyBehavior
            rb.AddForce(transform.up * 30f);
            rb.AddForce(transform.forward * 30f);

            // Detection
            RoundStatManager.Instance.HitObstacle();

            // Destroy
            StartCoroutine("destroyTimer");
        }

        public IEnumerator destroyTimer()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
