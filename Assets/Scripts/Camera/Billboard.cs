using UnityEngine;

//Following: https://youtu.be/_LRZcmX_xw0
public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera cameraToFollow;

    void Start()
    {
        cameraToFollow = Camera.main;
        //SetCamera();
    }

    void LateUpdate()
    {
        transform.LookAt(cameraToFollow.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
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

