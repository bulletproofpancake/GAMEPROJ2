using System;
using TMPro;
using UnityEngine;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {
        private TextMeshPro _moneyDisplay;
        [SerializeField] private float moneyToReceive;

        private void Start()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshPro>();
            _moneyDisplay.text = $"{moneyToReceive}";
        }
    }
}
