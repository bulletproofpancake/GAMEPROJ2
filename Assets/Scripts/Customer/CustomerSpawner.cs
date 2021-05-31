using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Customer
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform[] spawnLocations;

        private TimelineManager _timeline;

        private void Start()
        {
            _timeline = FindObjectOfType<TimelineManager>();
        }
    }
}