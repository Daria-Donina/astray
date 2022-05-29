using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class WindowManager : IService
    {
        private readonly List<WindowBase> _windows = new List<WindowBase>();
        private readonly WindowBase _defaultWindow;

        public WindowManager(List<WindowBase> windows, WindowBase defaultWindow)
        {
            _windows = windows;
            _defaultWindow = defaultWindow;

            foreach (var window in _windows)
            {
                window.WindowClosed += TryShowDefault;
            }
        }

        public void HideWindowsExcept(WindowBase window)
        {
            foreach (var item in _windows.Where(item => item != window))
            {
                item.CloseInstant();
            }
        }
        
        public void TryShowDefault(WindowBase window)
        {
            if (window != _defaultWindow)
                _defaultWindow.Show();
        }
        
        public void Dispose()
        {
            _windows.ForEach(x => x.Dispose());
        }
    }
}