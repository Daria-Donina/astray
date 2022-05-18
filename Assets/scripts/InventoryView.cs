using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _slotsContainer;
        [SerializeField] private RectTransform _root;

        public void UpdateVisual(InventoryItem newItem)
        {
            SetPosition(newItem);
        }

        private void SetPosition(InventoryItem newItem)
        {
            var itemTransform = newItem.transform;
            
            itemTransform.SetParent(_slotsContainer.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_root);
            itemTransform.localPosition = Vector3.zero;
            newItem.SetSortingLayer("UI");
            
            itemTransform.gameObject.AddComponent<LayoutElement>();
        }
    }
}