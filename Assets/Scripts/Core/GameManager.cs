using System.Collections;
using System.Collections.Generic;
using Stations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float levelDuration;
    public StationData[] stations;

    public StationData RandomizeStation()
    {
        var station = stations[Random.Range(0,stations.Length)];
        return station;
    }
}
