using Customer;
using UnityEngine;

namespace Core
{
    public class Obstacle : MonoBehaviour
    {
        private CustomerManagerPayment _customerManager;

        private void Start()
        {
            _customerManager = FindObjectOfType<CustomerManagerPayment>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
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
