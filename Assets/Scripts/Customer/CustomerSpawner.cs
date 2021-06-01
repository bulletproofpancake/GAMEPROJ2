using System;
using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        public Seats[] seat;
        public int Index { get; set; }

        private TimelineManager _timeline;

        private void Start()
        {
            _timeline = FindObjectOfType<TimelineManager>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Spawn();
        }

        public void Spawn()
        {
            //TODO: SWITCH TO AN OBJECT POOL
            //TODO: SPAWN CUSTOMERS WHEN JEEP COLLIDES
            if (Index < seat.Length)
            {
                if (!seat[Index].isTaken)
                {
                    var customer = Instantiate(prefab, parent);
                    customer.transform.position = seat[Index].transform.position;
                    customer.GetComponent<CustomerHand>().Spawner = this;
                    customer.GetComponent<CustomerHand>().seatTaken = Index;
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