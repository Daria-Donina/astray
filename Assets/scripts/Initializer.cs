using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private WorldView _world;
        
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        
        private void Awake()
        {
            _services.Add(typeof(InventoryPresenter), new InventoryPresenter(_world.InventoryView, _world.WorldItems));
        }
    }
}