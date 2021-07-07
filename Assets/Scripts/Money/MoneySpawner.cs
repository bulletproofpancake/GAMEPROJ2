using UnityEngine;

namespace Money
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        [SerializeField] private Transform spawnPosition;
        private Transform _parent;
        private MoneyManager _moneyManager;

        private void Start()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
        }

        public void SpawnMoney()
        {
            // if(_moneyManager.customer != null)
            //     spawnPosition = _moneyManager.customer.transform;
            // AudioManager.Instance.Play("CoinsPicked");
            var money = Instantiate(data.Prefab, spawnPosition);
            _moneyManager.AddMoney(data.Value, money);
        }
    }

}