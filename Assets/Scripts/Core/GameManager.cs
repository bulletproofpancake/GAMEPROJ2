using System.Collections.Generic;
using Stations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float levelDuration;
    public List<StationData> stations;

    public StationData RandomizeStation()
    {
        var station = stations[Random.Range(0,stations.Count)];
        return station;
    }

    public void RemoveStation()
    {
        if (stations.Count == 1)
        {
            Debug.LogWarning("Game Over");
            return;
        }
        
        stations.Remove(stations[0]);
        print(stations[0]);
    }
}
