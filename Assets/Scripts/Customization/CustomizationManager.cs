using UnityEngine;

namespace Customization
{
    public class CustomizationManager : Singleton<CustomizationManager>
    {
        [SerializeField] private CustomizationInfo activeCustomization;
        public MeshRenderer meshRenderer;

        private void Update()
        {
            if (activeCustomization != null && meshRenderer != null)
            {
                meshRenderer.material = activeCustomization.MaterialItem;
            }
        }
    }
}
