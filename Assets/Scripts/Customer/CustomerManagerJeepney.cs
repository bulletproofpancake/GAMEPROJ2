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
        private bool _isTutorialDone;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            if (GameObject.FindWithTag("Player"))
            {
                _playerTransform = GameObject.FindWithTag("Player").transform;
                _hasPlayer = true;
                Invoke("StartSpawning", 1f);
            }
            else
            {
                _hasPlayer = false;
            }
        }

        private void StartSpawning()
        {
            StartCoroutine(SpawnCustomers());
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
            
            if (!_isTutorialDone)
            {
                _gameManager.CallTutorial();
                _isTutorialDone = true;
            }
        }

        public void Jump()
        {
            AudioManager.Instance.Play("ParaPo");
            Debug.LogWarning("Jump");
            // var passenger = _gameManager.passengersList[0];
            // if (_gameManager.passengersList == null)
            // {
            //     passenger = Instantiate(passengers[Random.Range(0, passengers.Length)], parent);
            //     _gameManager.passengersList.Add(passenger);
            // }
            // if (passenger == null)
            // {
            //     passenger = _gameManager.passengersList[1];
            //     _gameManager.passengersList.Remove(_gameManager.passengersList[0]);
            // }
            if (_gameManager.passengersList.Count > 0)
            {
                GameObject passenger = _gameManager.passengersList[0];
                if (passenger == null)
                {
                    passenger = _gameManager.passengersList[1];
                    _gameManager.passengersList.Remove(_gameManager.passengersList[0]);
                }
                passenger.GetComponent<CustomerPassenger>().hasJumped = true;
                passenger.transform.position = new Vector3(_playerPosition.x, _playerPosition.y + _jumpHeight, _playerPosition.z - 5);
                passenger.SetActive(true);
                //
                // RigidbodyBehavior
                int side;
                side = Random.Range(1, 2);
                if (side == 1)
                { passenger.GetComponent<Rigidbody>().AddForce(transform.right * 100f); }
                if (side == 2)
                { passenger.GetComponent<Rigidbody>().AddForce(transform.right * -100f); }

                
                _gameManager.RemovePassenger(_gameManager.passengersList[0]);
            }
            else
            {
                var passenger = Instantiate(passengers[Random.Range(0, passengers.Length)], parent);
                _gameManager.passengersList.Add(passenger);
                passenger.GetComponent<CustomerPassenger>().hasJumped = true;
                passenger.transform.position = new Vector3(_playerPosition.x, _playerPosition.y + _jumpHeight, _playerPosition.z - 5);
                passenger.SetActive(true);
                _gameManager.RemovePassenger(_gameManager.passengersList[0]);
            }
            
        }
        
    }
}