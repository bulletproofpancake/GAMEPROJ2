using System.Collections.Generic;
using Stations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float levelDuration;
    public List<StationData> stations;
    public bool isJeepActive;

    public StationData RandomizeStation()
    {
        var station = stations[Random.Range(0,stations.Count)];
        return station;
    }

    public void RemoveStation()
    {
        //FIXME: FIX MONEY COSTS WHEN REMOVING STATIONS
        if (stations.Count == 1)
        {
            Debug.LogWarning("Game Over");
            return;
        }
        
        stations.Remove(stations[0]);
        print(stations[0]);
    }

    public void GameOver()
    {
        //TODO: IMPLEMENT GAME OVER SEQUENCE
        SceneLoader.Instance.LoadNextScene();
        print("Game Over");
    }
    
}
