using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DialogWindow : WindowBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _textLabel;
        
        public void UpdateVisual(Sprite speakerImage, string text)
        {
            _image.sprite = speakerImage;
            _textLabel.text = text;
        }
    }
}