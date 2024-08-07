using FlaUI.Core.AutomationElements;
using Framework.Utils;
using System;

namespace Framework.App
{
    public class WinManager
    {
        public Window Window { get; private set; }

        public WinManager(Window window)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));
        }

        public void Close()
        {
            Logger.Info("Window Closed");
            Window.Close();
        }
    }
}
