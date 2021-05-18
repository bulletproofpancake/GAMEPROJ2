using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class MoneyLoader : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            LoadSprite();
            print($"{data.Value}");
        }

        private void LoadSprite()
        {
            _spriteRenderer.sprite = data.Icon;
        }
    }
}
