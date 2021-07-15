using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicator : Singleton<SpeedIndicator>
{
    public Image indicator;
    public Image[] speed;
    private int index;

    public void SpeedUp()
    {
        index++;
        if (index <= 2)
            indicator.transform.position = speed[index].transform.position + new Vector3(12.5f,12.5f);
        else
        {
            index = 2;
        }
    }

    public void Stop()
    {
        index = 0;
        indicator.transform.position = speed[index].transform.position + new Vector3(12.5f,12.5f);
    }
}
