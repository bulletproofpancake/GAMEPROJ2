using UnityEngine;

namespace Money
{
    [CreateAssetMenu(fileName = "MoneyData", menuName = "Data/ Money")]
    public class MoneyData : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField] private int value;
        public int Value => value;
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}