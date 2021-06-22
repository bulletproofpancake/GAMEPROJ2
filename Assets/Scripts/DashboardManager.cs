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
        dashboardCanvas.SetActive(!dashboardCanvas.activeSelf);
    }
}
