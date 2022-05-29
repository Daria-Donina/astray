namespace DefaultNamespace
{
    public static class WindowExtensions
    {
        private static WindowManager _windowService => Initializer.GetService<WindowManager>();

        public static void CloseAllWindowsExcept(this WindowBase window)
        {
            _windowService.HideWindowsExcept(window);
        }
    }
}