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
            //TODO: REJECT IF THE PLAYED DOES NOT HAVE ENOUGH FUNDS
            RoundStatManager.Instance.Spend(customizationInfo.Cost);
            customizationInfo.hasBeenBought = true;
        }
    }
}
