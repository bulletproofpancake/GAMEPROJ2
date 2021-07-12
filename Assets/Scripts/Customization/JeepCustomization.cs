using UnityEngine;

namespace Customization
{
    public class JeepCustomization : MonoBehaviour
    {
        private void Start()
        {
            CustomizationManager.Instance.meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}