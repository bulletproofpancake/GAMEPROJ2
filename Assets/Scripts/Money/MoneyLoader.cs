using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class MoneyLoader : MonoBehaviour
    {
        [SerializeField] private MoneyData data;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            LoadSprite();
            print($"{data.Value}");
        }

        private void LoadSprite()
        {
            _image.sprite = data.Icon;
        }
    }
}
