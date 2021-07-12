using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Customization
{
    public class CustomizationShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject shopDisplay;
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        public List<CustomizationShopItem> shopItems;
        private bool _isOpen;

        private void Awake()
        {
            shopItems = new List<CustomizationShopItem>();
            shopDisplay.SetActive(_isOpen);
        }

        private void Update()
        {
            moneyDisplay.text = $"Php: {RoundStatManager.Instance.totalMoney}";
        }

        public void ToggleDisplay()
        {
            AudioManager.Instance.Play("Click");
            _isOpen = !_isOpen;
            shopDisplay.SetActive(_isOpen);
        }
        
    }
}