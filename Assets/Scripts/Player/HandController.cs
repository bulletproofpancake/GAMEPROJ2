using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
   public class HandController : MonoBehaviour
    {
        private Camera _camera;

        // Start is called before the first frame update
        private void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
   
}