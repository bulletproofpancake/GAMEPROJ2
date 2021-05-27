using UnityEngine;

namespace Stations
{
    [CreateAssetMenu(fileName = "Station", menuName = "Data/New Station")]
    public class StationData : ScriptableObject
    {
        [SerializeField] private float cost;
        public float Cost => cost;
    }
}