using UnityEngine;

namespace Customization
{
    [CreateAssetMenu(fileName = "CustomizationInfo", menuName = "Data/Customization Info")]
    public class CustomizationInfo : ScriptableObject
    {
        [SerializeField] private int cost;
        public int Cost => cost;
        [SerializeField] private Material materialItem;
        public Material MaterialItem => materialItem;
    }
}