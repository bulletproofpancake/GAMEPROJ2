using System;
using UnityEngine;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {
        [SerializeField] private GameObject paymentPopUp;
        [SerializeField] private Transform[] slots;
        [SerializeField] private GameObject[] money;
        public void OnMouseDown()
        {
            print(true);
            if (paymentPopUp.activeSelf)
            {
                paymentPopUp.SetActive(false);
                return;
            }
            paymentPopUp.SetActive(true);
            foreach (var slot in slots)
            {
                Instantiate(money[0], slot.position, Quaternion.identity);
            }
            
        }
    }
}
