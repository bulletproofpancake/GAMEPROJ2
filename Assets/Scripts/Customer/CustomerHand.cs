using System;
using UnityEngine;

namespace Customer
{
    public class CustomerHand : MonoBehaviour
    {
        [SerializeField] private GameObject paymentPopUp;
        
        public void OnMouseDown()
        {
            if (paymentPopUp.activeSelf)
            {
                paymentPopUp.SetActive(false);
                return;
            }
            paymentPopUp.SetActive(true);
        }
    }
}
