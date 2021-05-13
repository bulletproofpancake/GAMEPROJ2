using System;
using UnityEngine;

namespace Money
{
    public class MoneyLoader : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private float value;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = sprite;
        }
    }
}
