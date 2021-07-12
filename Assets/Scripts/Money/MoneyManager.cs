using System.Collections;
using System.Collections.Generic;
using Core;
using Customer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay, roundMoneyDisplay;
        public TextMeshProUGUI giveMoneyIndicator;
        private int _currentTotal;
        private List<GameObject> _moneyPrefabs;
        private Vector3 _startingPosition;
        private Canvas _canvas;
        
        public CustomerHand customer;
        public float paymentReceived;

        public float stationCost;

        [SerializeField] private Button giveButton;
        [SerializeField] private Button clearButton;
        private GameManager _gameManager;
        private bool _isTutorialDone;
        [SerializeField] private int moneyFloor;
        public bool hitFloor;
        private void Awake()
        {
            _moneyPrefabs = new List<GameObject>();
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _startingPosition = transform.position;
            moneyDisplay.text = string.Empty;
            //SetCamera();
            StartCoroutine(MoneyFloor());
        }

        private void SetCamera()
        {
            var cameras = FindObjectsOfType<Camera>();

            foreach (var cam in cameras)
            {
                if (cam.CompareTag("2D Camera"))
                {
                    _canvas.worldCamera = cam;
                }
            }
        }

        private void Update()
        {

            if (customer == null )
            {
                if(_currentTotal != 0)
                {
                    giveButton.gameObject.SetActive(true);
                    clearButton.gameObject.SetActive(true);
                }
                else
                {
                    giveButton.gameObject.SetActive(false);
                    clearButton.gameObject.SetActive(false);
                }
            }

            if (customer != null)
            {
                moneyDisplay.text = $"{paymentReceived} - {stationCost} = {_currentTotal}";
             
                giveButton.gameObject.SetActive(true);
                clearButton.gameObject.SetActive(true);
                
                if (customer.MoneyToReceive == 0)
                {
                    giveMoneyIndicator.text = "Receive Money";
                }
            }
            else
                moneyDisplay.text = _currentTotal == 0 ? string.Empty : $"{_currentTotal}";

            roundMoneyDisplay.text = $"PHP: {RoundStatManager.Instance.CalculateNet()}";
            if (RoundStatManager.Instance.CalculateNet() <= moneyFloor)
            {
                hitFloor = true;
            }
        }

        IEnumerator MoneyFloor()
        {
            while(!hitFloor){
                yield return null;
            }
            _gameManager.GameOver();
        }
        
        private void ReturnToStartingPosition()
        {
            transform.position = _startingPosition;
        }
        
        public void AddMoney(int moneyToAdd,GameObject moneyObject)
        {
            _currentTotal += moneyToAdd;
            _moneyPrefabs.Add(moneyObject);
        }
        
        public void GiveMoney()
        {
            if (!_isTutorialDone)
            {
                _gameManager.CallTutorial();
                _isTutorialDone = true;
            }
            
            if (customer == null)
            {
                Debug.LogWarning("No Customer Found");
                return;
            }
            AudioManager.Instance.Play("CoinsReturned");
            print($"Gave {_currentTotal} to passenger");
            
            if (_currentTotal == customer.MoneyToReceive)
            {
                Debug.LogWarning("Correct");
                AudioManager.Instance.Play("CorrectCompute");
                customer.ReceivePayment(true);
            }
            else
            {
                AudioManager.Instance.Play("WrongCompute");
                Debug.LogWarning("Wrong");
                customer.ReceivePayment(false);
            }
            
            ClearMoney();

            //After giving the money to the customer
            //The money manager removes references to that customer
            customer = null;
        }

        public void ClearMoney()
        {
            AudioManager.Instance.Play("CoinsReturned");
            moneyDisplay.text = string.Empty;
            print($"Cleared {_currentTotal} pesos");
            _currentTotal = 0;
            foreach (var moneyPrefab in _moneyPrefabs)
            {
                Destroy(moneyPrefab);
            }
            ReturnToStartingPosition();
        }

        private void OnMouseUp()
        {
            ReturnToStartingPosition();
        }
    }
}
