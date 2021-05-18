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
        
        public float moneyToReceive;

        private void Awake()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            moneyToReceive = Random.Range(1, 11);
            _moneyDisplay.text = $"{moneyToReceive}";
        }

        private void Update()
        {
            //Changes the sprite color if this is selected by the money manager
            _spriteRenderer.color = _moneyManager.customer == this ? Color.yellow : Color.white;
        }

        private void OnMouseDown()
        {
            _moneyManager.customer = this;
        }

    }
}
