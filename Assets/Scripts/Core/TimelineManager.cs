using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private Slider timelineDisplay;

        private void Update()
        {
            timelineDisplay.value += Time.deltaTime;
            print(Time.deltaTime);
        }
    }
}