using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Money
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        private float _currentTotal;

        private void Update()
        {
            //if there's no money in the scene, UI is empty
            moneyDisplay.text = _currentTotal <= 0 ? string.Empty : $"{_currentTotal:0}";
        }

        public void AddMoney(float moneyToAdd)
        {
            _currentTotal += moneyToAdd;
        }
        
        public void GiveMoney()
        {
            
        }

        public void ClearMoney()
        {
            
        }
    }
}
