using System.Collections.Generic;
using UnityEngine;

namespace Customization
{
    public class ShopPageManager : MonoBehaviour
    {
        public CustomizationShopPage[] customizationShopPages;
        [SerializeField] private int _index;

        private void Awake()
        {
            customizationShopPages = GetComponentsInChildren<CustomizationShopPage>();
            DisablePage();
        }
        
        private void OnEnable()
        {
            customizationShopPages[_index].gameObject.SetActive(true);
        }

        public void ShowPreviousPage()
        {
            AudioManager.Instance.Play("Click");
            DisablePage();
            if(_index > 0)
            {
                _index--;
            }
            else
            {
                _index = customizationShopPages.Length-1;
            }
            customizationShopPages[_index].gameObject.SetActive(true);
        }
        
        public void ShowNextPage()
        {
            AudioManager.Instance.Play("Click");
            DisablePage();
            if(_index < customizationShopPages.Length-1)
            {
                _index++;
            }
            else
            {
                _index = 0;
            }
            customizationShopPages[_index].gameObject.SetActive(true);
        }

        private void DisablePage()
        {
            foreach (var page in customizationShopPages)
            {
                page.gameObject.SetActive(false);
            }
        }
        
    }
}