using TMPro;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeManager :Singleton<UpgradeManager>
    {
        private int _totalMoney;
        private int _roundMoney;
        private TextMeshProUGUI _moneyDisplay;

        private void Start()
        {
            _moneyDisplay = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void AddMoney(int moneyToAdd)
        {
            _roundMoney += moneyToAdd;
        }

        public void EndRound()
        {
            _totalMoney += _roundMoney;
            _roundMoney = 0;
        }

        private void Update()
        {
            _moneyDisplay.text = $"{_roundMoney}";
        }
    }
}