using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : Singleton<SpeedIndicator>
{
    public Image indicator;
    public Image[] speed;
    private int index;
    //private float offset = 18.75f;
    // public void SpeedUp()
    // {
    //     index++;
    //     if (index <= 2)
    //         indicator.transform.position = speed[index].transform.position + new Vector3(25f,25f);
    //     else
    //     {
    //         index = 2;
    //     }
    // }
    //
    // public void Stop()
    // {
    //     index = 0;
    //     indicator.transform.position = speed[index].transform.position + new Vector3(25f,25f);
    // }

    public void SetIndicator(float gear)
    {
        indicator.transform.position = speed[(int)gear].transform.position + new Vector3(25f,25f);
    }
    
}
