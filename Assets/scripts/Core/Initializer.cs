using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private WorldView _world;
        [SerializeField] private Dialogs _dialogData;
        [SerializeField] private CharactersData _charactersData;
        
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        
        private void Awake()
        {
            _services.Add(typeof(InventoryPresenter), new InventoryPresenter(_world.InventoryView, _world.WorldItems));
            _services.Add(typeof(DialogPresenter), new DialogPresenter(_world.DialogWindow));
            _services.Add(typeof(WindowManager), new WindowManager(new List<WindowBase>()
            {
                _world.DialogWindow,
                _world.InventoryView
            }, 
                _world.InventoryView));
            
            _dialogData.Initialize();
            _charactersData.Initialize();
        }

        public static T GetService<T>() where T : class, IService
        {
            var type = typeof(T);
            
            if (_services.ContainsKey(type))
                return _services[type] as T;

            return null;
        }

        private void OnDestroy()
        {
            _services.Values.ToList().ForEach(x => x.Dispose());
        }
    }
}