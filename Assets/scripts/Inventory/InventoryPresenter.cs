using System.Collections.Generic;
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
    }
}