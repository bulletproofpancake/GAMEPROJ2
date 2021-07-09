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
            if(gameObject.CompareTag("Obstacle"))
                AudioManager.Instance.Play("Crash");
            else if (gameObject.CompareTag("Barrier"))
                AudioManager.Instance.Play("BarrierCrash");
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
            int side;
            side = Random.Range(0, 1);
            if (side == 0)
            { rb.AddForce(transform.right * 1000f); }
            if (side == 1)
            { rb.AddForce(transform.right * -1000f); }
            rb.AddForce(transform.up * 200f);
            rb.AddForce(transform.forward * 100f);

            // Detection
            RoundStatManager.Instance.HitObstacle();

            // Destroy
            if(gameObject.CompareTag("Obstacle"))
                Destroy(gameObject);
            else if (gameObject.CompareTag("Barrier"))
                StartCoroutine(destroyTimer());
        }

        public IEnumerator destroyTimer()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
