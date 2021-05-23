using System.Collections;
using Money;
using TMPro;
using UnityEngine;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {
        private TextMeshPro _moneyDisplay;
        private MoneyManager _moneyManager;
        private SpriteRenderer _spriteRenderer;

        public bool isGivingMoney;
        public float moneyToGive;
        public float moneyToReceive;

        private void Awake()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            GivePayment();
        }

        private void GivePayment()
        {
            isGivingMoney = true;
            _moneyDisplay.text = moneyToGive.ToString();
        }

        private void Update()
        {
            //Changes the sprite color if this is selected by the money manager
            _spriteRenderer.color = _moneyManager.customer == this ? Color.yellow : Color.white;
        }

        private void OnMouseDown()
        {
            if (isGivingMoney)
            {
                _moneyManager.paymentReceived = moneyToGive;
                _moneyDisplay.text = string.Empty;
                isGivingMoney = false;
            }
            
            _moneyManager.customer = this;
            
            print(isGivingMoney);
        }

    }
}
