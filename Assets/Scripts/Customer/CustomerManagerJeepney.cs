using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerManagerJeepney : MonoBehaviour
    {
        
        [SerializeField] private Transform parent;
        [SerializeField] private float timeToSpawnCap;
        [SerializeField] private Transform sideWalk;
        [SerializeField] private float spawnDistanceFromPlayer;
        [SerializeField] private GameObject[] passengers;
        private Transform _playerTransform;
        private Vector3 _playerPosition;
        private bool _hasPlayer;
        private GameManager _gameManager;
        private float _jumpHeight = 2.0f;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            if (GameObject.FindWithTag("Player"))
            {
                _playerTransform = GameObject.FindWithTag("Player").transform;
                _hasPlayer = true;
                StartCoroutine(SpawnCustomers());
            }
            else
            {
                _hasPlayer = false;
            }
        }

        private void Update()
        {
            if(_hasPlayer)
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
            passenger.transform.position = new Vector3(sideWalk.position.x, _playerPosition.y, _playerPosition.z + spawnDistanceFromPlayer);
            
            if (!_gameManager.hasCompletedTutorial)
            {
                _gameManager.CallTutorial();
            }
        }

        public void Jump()
        {
            Debug.LogWarning("Jump");
            var passenger = _gameManager.passengersList[0];
            passenger.GetComponent<CustomerPassenger>().hasJumped = true;
            passenger.transform.position = new Vector3(_playerPosition.x, _playerPosition.y + _jumpHeight, _playerPosition.z - 5);
            passenger.SetActive(true);
            _gameManager.RemovePassenger(_gameManager.passengersList[0]);
        }
        
    }
}