using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public float zSpawn = 0;
    public float roadLength = 10;
    public int numberofTiles = 2;
    private List<GameObject> activeRoads = new List<GameObject>();

    public Transform playerTransform;

    void Start()
    {
        for (int i = 0; i < numberofTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, roadPrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 30 > zSpawn - (numberofTiles * roadLength))
         {
            SpawnTile(Random.Range(0, roadPrefabs.Length));
            DeleteTile();
         }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(roadPrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeRoads.Add(go);
        zSpawn += roadLength;
    }
    
    private void DeleteTile()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
