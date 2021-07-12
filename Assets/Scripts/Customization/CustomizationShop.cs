using UnityEngine;

namespace Customization
{
    public class CustomizationShop : MonoBehaviour
    {
        [SerializeField] private CustomizationInfo customizationInfo;
        
        public void SelectCustomization()
        {
            CustomizationManager.Instance.SetInfo(customizationInfo);
        }

        public void BuyCustomization()
        {
            RoundStatManager.Instance.Spend(customizationInfo.Cost);
        }
    }
}
