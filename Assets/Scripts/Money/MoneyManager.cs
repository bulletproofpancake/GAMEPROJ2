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
            giveButton.gameObject.SetActive(customer != null);
            clearButton.gameObject.SetActive(customer != null);
            
            if (customer != null)
            {
                moneyDisplay.text = $"{paymentReceived} - {stationCost} = {_currentTotal}";
                
                if (customer.MoneyToReceive == 0)
                {
                    giveMoneyIndicator.text = "Receive Money";
                }
            }
            else
                moneyDisplay.text = _currentTotal == 0 ? string.Empty : $"{_currentTotal}";

            roundMoneyDisplay.text = $"PHP: {RoundStatManager.Instance.CalculateNet()}";
        }

        IEnumerator MoneyFloor()
        {
            while (RoundStatManager.Instance.net > moneyFloor)
            {
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
