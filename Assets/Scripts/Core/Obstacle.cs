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
            if(gameObject.CompareTag("Obstacle") || gameObject.CompareTag("MovingObstacle"))
                AudioManager.Instance.Play("Crash");
            else if (gameObject.CompareTag("Barrier"))
                AudioManager.Instance.Play("BarrierCrash");


            GetComponent<Collider>().enabled = false;
            
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
            side = Random.Range(1, 2);
            if (side == 1)
            { rb.AddForce(transform.right * 100f); }
            if (side == 2)
            { rb.AddForce(transform.right * -100f); }
            rb.AddForce(transform.up * 200f);
            rb.AddForce(transform.forward * 100f);

            // Detection
            RoundStatManager.Instance.HitObstacle();

            // Destroy
            // if(gameObject.CompareTag("Obstacle"))
            //     Destroy(gameObject);
            // else if (gameObject.CompareTag("Barrier"))
            StartCoroutine(destroyTimer());
        }

        public IEnumerator destroyTimer()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
