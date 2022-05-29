using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WorldView : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] private List<InventoryItem> _worldItems;
        [SerializeField] private InventoryView _inventoryView;
        
        [Header("Dialog")]
        [SerializeField] private DialogWindow _dialogWindow;

        public List<InventoryItem> WorldItems => _worldItems;
        public InventoryView InventoryView => _inventoryView;
        public DialogWindow DialogWindow => _dialogWindow;
    }
}