using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Following: https://youtu.be/_LRZcmX_xw0
public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera cameraToFollow;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCamera()
    {
        //Goes through all cameras in the scene to look for the 2D camera
        var cameras = FindObjectsOfType<Camera>();
        
        foreach (var cam in cameras)
        {
            if (cam.CompareTag("2D Camera"))
            {
                cameraToFollow = cam;
            }
        }
        
    }
    
}
