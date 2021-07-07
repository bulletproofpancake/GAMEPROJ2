using Customer;
using UnityEngine;

namespace Core
{
    public class Obstacle : MonoBehaviour
    {
        private CustomerManagerPayment _customerManager;
        [SerializeField] private TutorialInfo tutorial;
        private GameManager _gameManager;

        private void Start()
        {
            _customerManager = FindObjectOfType<CustomerManagerPayment>();
            _gameManager = FindObjectOfType<GameManager>();
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
            RoundStatManager.Instance.HitObstacle();
            Destroy(gameObject);
        }
    }
}
