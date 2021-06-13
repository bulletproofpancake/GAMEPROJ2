using UnityEngine;

namespace Money
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        [SerializeField] private Transform parent;
        private MoneyManager _moneyManager;

        private void Start()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
        }

        public void SpawnMoney()
        {
            //TODO: SWITCH TO AN OBJECT POOL
            var money = Instantiate(data.Prefab, parent);
            _moneyManager.AddMoney(data.Value, money);
        }
    }

}