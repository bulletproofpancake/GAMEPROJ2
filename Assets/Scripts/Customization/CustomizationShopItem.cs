using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Customization
{
    public class CustomizationShopItem : MonoBehaviour
    {
        [SerializeField] private CustomizationInfo customizationInfo;
        [SerializeField] private Image preview;
        [SerializeField] private TextMeshProUGUI buyDisplay;

        public bool bypassCosts;

        private CustomizationShopManager _shopManager;

        private void Awake()
        {
            _shopManager = FindObjectOfType<CustomizationShopManager>();
        }

        private void Start()
        {
            //_shopManager.shopItems.Add(this);
        }

        private void Update()
        {
            buyDisplay.text = customizationInfo.hasBeenBought ? "Select" : $"Buy: {customizationInfo.Cost}";
        }

        public void ActivateItem()
        {
            if (customizationInfo.hasBeenBought)
            {
                SelectCustomization();
            }
            else
            {
                BuyCustomization();
            }
        }
        
        private void SelectCustomization()
        {
            CustomizationManager.Instance.SetInfo(customizationInfo);
        }

        private void BuyCustomization()
        {
            if (bypassCosts)
            {
                customizationInfo.hasBeenBought = true;
                return;
            }
            if (RoundStatManager.Instance.Spend(customizationInfo.Cost))
            {
                customizationInfo.hasBeenBought = true;
            }
            else
            {
                print("Not enough money");
            }
            
        }
    }
}
