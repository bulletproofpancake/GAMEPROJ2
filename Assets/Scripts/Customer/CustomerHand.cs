using System.Collections;
using Core;
using Money;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {

        #region Hidden In Inspector
        
        private GameManager _gameManager;
        [SerializeField] private MoneyManager _moneyManager;
        
        //private TextMeshProUGUI _moneyDisplay;
        private Image _image;

        
        private int _moneyToGive;
        
        public int MoneyToReceive { get; private set; }
        
        //private StationData _stationSelected;

        private bool _hasReceivedPayment;

        [HideInInspector] public CustomerManagerPayment customerManager;
        public int SeatTaken { get; set; }
        
        public float TimeSpawned { get; set; }

        private TimelineManager _timeline;

        private CustomerManagerJeepney _customerManagerJeepney;
        
        private int _paymentCap;
        
        private int cost;
        #endregion

        #region ShownInInspector
        [SerializeField] private float patience;
        [SerializeField] private Sprite openHand;
        // public Button giveMoneyButton;
        // public TextMeshProUGUI giveMoneyText;
        #endregion
        private bool _isTutorialDone;


        private void Awake()
        {
            //_moneyDisplay = GetComponentInChildren<TextMeshProUGUI>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _image = GetComponent<Image>();
            _timeline = FindObjectOfType<TimelineManager>();
            _customerManagerJeepney = FindObjectOfType<CustomerManagerJeepney>();
        }

        private void Start()
        {
            //SelectStation();
            GivePayment();
        }

        private void OnDestroy()
        {
            customerManager.seats[SeatTaken].isTaken = false;
            customerManager.seatsTaken--;
        }

        // private void SelectStation()
        // {
        //     _stationSelected = _gameManager.RandomizeStation();
        //     _moneyToGive = _stationSelected.Cost + Random.Range(0, _paymentCap + 1);
        //     MoneyToReceive = _moneyToGive - _stationSelected.Cost;
        //     print($"{_stationSelected}");
        // }

        private void GivePayment()
        {
            _paymentCap = _gameManager.CustomerPaymentCap;
            print(_paymentCap);
            //cost = Random.Range(_gameManager.StationPayment, _paymentCap + 1);
            cost = Random.Range(_gameManager.StationCost, _paymentCap);
            _moneyToGive = cost + _paymentCap;
            MoneyToReceive = _moneyToGive - cost;
            //_moneyDisplay.text = $"{_moneyToGive}";
        }

        private void Update()
        {
            // if(_timeline.hasReachedStation && _timeline.stationReached == _stationSelected)
            // {
            //     print($"{this} left");
            //     StartCoroutine(Leave());
            // }

            //Activates the "Give Money" button when selected by the money manager
            //giveMoneyButton.gameObject.SetActive(_moneyManager.customer == this);

            if(!_hasReceivedPayment)
            {
                //Changes the sprite color if this is selected by the money manager
                _image.color = _moneyManager.customer == this ? Color.yellow : Color.white;
            }
        }

        public void GetSelected()
        {
            _moneyManager.paymentReceived = _moneyToGive;
            _moneyManager.stationCost = cost;
            //_moneyDisplay.text = string.Empty;
            _image.sprite = openHand;
            _moneyManager.customer = this;
            //_moneyManager.giveMoneyIndicator = giveMoneyText;
            if (!_isTutorialDone)
            {
                _gameManager.CallTutorial();
                _isTutorialDone = true;
            }
        }

        private IEnumerator Respond(bool isCorrect)
        {
            _image.color = isCorrect ? Color.green : Color.red;
            yield return new WaitForSeconds(1f);
            if (!isCorrect)
            {
                _hasReceivedPayment = false;
                yield break;
            }

            RoundStatManager.Instance.CollectPayment(cost);
            RoundStatManager.Instance.CompleteCustomer();
            customerManager.customers.Remove(this);
            _customerManagerJeepney.Jump();
            Destroy(gameObject);
        }

        public void Leave()
        {
            StartCoroutine(LeaveRoutine());
        }
        
        private IEnumerator LeaveRoutine()
        {
            _hasReceivedPayment = true;
            _image.color = Color.red;
            yield return new WaitForSeconds(1f);
            _hasReceivedPayment = false;
            customerManager.customers.Remove(this);
            _customerManagerJeepney.Jump();
            Destroy(gameObject);
        }
        
        public void ReceivePayment(bool isCorrect)
        {
            _hasReceivedPayment = true;
            StartCoroutine(Respond(isCorrect));
        }

    }
}
