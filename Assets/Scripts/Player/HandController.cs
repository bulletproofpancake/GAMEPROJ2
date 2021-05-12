using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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
            //Hand follows mouse position
        }
    }
   
}