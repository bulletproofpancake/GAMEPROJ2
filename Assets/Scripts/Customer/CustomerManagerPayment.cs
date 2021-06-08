using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerManagerPayment : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        public Seats[] seat;
        public int Index { get; set; }

        private TimelineManager timeline;

        private void Start()
        {
            timeline = FindObjectOfType<TimelineManager>();
        }

        private void Update()
        {
            //TODO: SPAWN CUSTOMERS WHEN JEEP COLLIDES
            if(Input.GetKeyDown(KeyCode.Space))
                Spawn();

        }

        public void Spawn()
        {
            //TODO: SWITCH TO AN OBJECT POOL
            //TODO: INDICATE ON TIMELINE WHEN CUSTOMER IS PICKED UP
            if (Index < seat.Length)
            {
                if (!seat[Index].isTaken)
                {
                    var customer = Instantiate(prefab, parent);
                    customer.transform.position = seat[Index].transform.position;
                    customer.GetComponent<CustomerHand>().customerManager = this;
                    customer.GetComponent<CustomerHand>().SeatTaken = Index;
                    customer.GetComponent<CustomerHand>().TimeSpawned = timeline.Display.value;
                    print($"{customer.GetComponent<CustomerHand>().TimeSpawned}");
                    seat[Index].isTaken = true;
                    //index always starts at zero so that all slots can be checked
                    Index = 0;
                }
                else
                {
                    Debug.LogWarning("Seat taken, looking for another");
                    Index++;
                    //recursion is done so that
                    //it will continue to spawn
                    //even if seats are taken
                    Spawn();
                }
            }
            else
            {
                Debug.LogWarning("No Seats left");
                Index = 0;
            }
        }
        
    }
}