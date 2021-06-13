using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerManagerPayment : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        
        public Seats[] seats;
        private int _index;
        
        public bool areSeatsFull;
        public int seatsTaken;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Spawn();

            areSeatsFull = seatsTaken == seats.Length;
        }

        public void Spawn()
        {
            //TODO: SWITCH TO AN OBJECT POOL
            if (_index < seats.Length)
            {
                if (!seats[_index].isTaken)
                {
                    var customer = Instantiate(prefab, parent);
                    customer.transform.position = seats[_index].transform.position;
                    customer.GetComponent<CustomerHand>().customerManager = this;
                    customer.GetComponent<CustomerHand>().SeatTaken = _index;
                    seats[_index].isTaken = true;
                    seatsTaken++;
                    //index always starts at zero so that all slots can be checked
                    _index = 0;
                }
                else
                {
                    Debug.LogWarning("Seat taken, looking for another");
                    _index++;
                    //recursion is done so that
                    //it will continue to spawn
                    //even if seats are taken
                    Spawn();
                }
            }
            else
            {
                Debug.LogWarning("No Seats left");
                _index = 0;
            }
        }
        
    }
}