using System.Collections;
using UnityEngine;

namespace Customer
{
    public class CustomerManagerJeepney : MonoBehaviour
    {
        
        [SerializeField] private Transform parent;
        [SerializeField] private float timeToSpawnCap;
        [SerializeField] private GameObject[] passengers;
        private Transform _playerTransform;
        private Vector3 _playerPosition;

        private void Start()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            StartCoroutine(SpawnCustomers());
        }

        private void Update()
        {
            _playerPosition = _playerTransform.position;
        }

        private IEnumerator SpawnCustomers()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0, timeToSpawnCap));
                Spawn();
            }
        }
        
        private void Spawn()
        {
            var passenger = Instantiate(passengers[Random.Range(0, passengers.Length)], parent);
            //TODO: SET X VALUE TO SIDEWALK
            passenger.transform.position = new Vector3(_playerPosition.x, _playerPosition.y, _playerPosition.z + 20);
        }
    }
}