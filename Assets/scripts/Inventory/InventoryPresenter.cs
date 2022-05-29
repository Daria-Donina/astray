using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class InventoryPresenter : IService
    {
        private InventoryView _view;
        private List<InventoryItem> _collectedItems = new List<InventoryItem>();
        
        public InventoryPresenter(InventoryView view, List<InventoryItem> worldItems)
        {
            _view = view;
            worldItems.ForEach(x => x.ItemTouched += Add);
        }

        private void Add(InventoryItem item)
        {
            _collectedItems.Add(item);
            _view.UpdateVisual(item);
            item.Dispose();
        }

        public bool HasItem(string itemName)
        {
            return _collectedItems.Any(x => x.Name == itemName);
        }

        public void Dispose()
        {
        }
    }
}