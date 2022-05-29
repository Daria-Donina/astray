using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private string[] _detectedLayers;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Space]
        [SerializeField] private string _name;
        
        private LayerMask _layerMask;

        private void Awake()
        {
            _layerMask = LayerMask.GetMask(_detectedLayers);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) > 0) 
            {
                ItemTouched?.Invoke(this);
            }
        }
        
        public event Action<InventoryItem> ItemTouched;
        public string Name => _name;

        public void Dispose()
        {
            ItemTouched = null;
        }

        public void SetSortingLayer(string layerName)
        {
            _spriteRenderer.sortingLayerName = layerName;
        }
    }
}