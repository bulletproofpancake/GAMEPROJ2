using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
   public class HandController : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool isLerped;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (!isLerped)
            {
                transform.position = mousePos;
            }
            else
                transform.position = Vector3.Lerp(transform.position, mousePos, moveSpeed * Time.deltaTime);
        }
    }
   
}