using UnityEngine;

namespace Customization
{
    public class CustomizationShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject shopDisplay;
        private bool _isOpen;

        public void ToggleDisplay()
        {
            AudioManager.Instance.Play("Click");
            _isOpen = !_isOpen;
            shopDisplay.SetActive(_isOpen);
        }
        
    }
}