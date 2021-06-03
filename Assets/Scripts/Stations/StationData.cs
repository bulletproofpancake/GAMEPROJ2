using UnityEngine;

namespace Stations
{
    [CreateAssetMenu(fileName = "Station", menuName = "Data/New Station")]
    public class StationData : ScriptableObject
    {
        [SerializeField] private int cost;
        public int Cost => cost;
        
        [SerializeField] private Color indicator;
        public Color Indicator => indicator;
    }
}