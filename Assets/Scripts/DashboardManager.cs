using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardManager : MonoBehaviour
{
    [SerializeField] private GameObject dashboardCanvas;

    private void Start()
    {
        dashboardCanvas.SetActive(false);
    }

    public void ToggleDashboard()
    {
        //if the dashboard is open, it closes when the button is clicked and vice-versa
        dashboardCanvas.SetActive(!dashboardCanvas.activeSelf);
    }
}
