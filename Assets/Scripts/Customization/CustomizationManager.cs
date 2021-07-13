using System.Collections.Generic;
using UnityEngine;

namespace Customization
{
    public class CustomizationManager : Singleton<CustomizationManager>
    {
        [SerializeField] private CustomizationInfo activeCustomization;
        public MeshRenderer meshRenderer;
        
        // Add references of the customizations here so that the player does not need to buy purchased skins again
        public List<CustomizationInfo> availableCustomizations;

        private void Update()
        {
            if (activeCustomization != null && meshRenderer != null)
            {
                meshRenderer.material = activeCustomization.MaterialItem;
            }
        }

        public void SetInfo(CustomizationInfo customizationInfo)
        {
            activeCustomization = customizationInfo;
        }
    }
}
