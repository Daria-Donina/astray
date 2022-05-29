using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
    public static class UnlockManager
    {
        private static readonly InventoryPresenter _inventoryService;

        static UnlockManager()
        {
            _inventoryService = Initializer.GetService<InventoryPresenter>();
        }
        
        public static bool IsLocked(IEnumerable<Unlock> unlocks)
        {
            return unlocks.Any(IsLocked);
        }
        
        public static bool IsLocked(Unlock unlock)
        {
            switch (unlock.UnlockType)
            {
                case UnlockType.ItemInInventory:
                    return !_inventoryService.HasItem(unlock.UnlockValue);
                default:
                    return true;
            }
        }
    }
}