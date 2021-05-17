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
        private float value;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            LoadData();
            print($"{value}");
        }

        private void LoadData()
        {
            _spriteRenderer.sprite = data.Icon;
            value = data.Value;
        }
    }
}
