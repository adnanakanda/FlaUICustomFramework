using FlaUI.Core.AutomationElements;
using Framework.FileUtils;
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
            Logger.Info("Closing window");
            Window.Close();
            Logger.Info("Window closed.");
        }
    }
}
