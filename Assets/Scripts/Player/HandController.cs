using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    //Following: https://youtu.be/0jTPKz3ga4w
    public class HandController : MonoBehaviour
    {
        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                transform.position = hit.point;
            }
        }
    }
   
}