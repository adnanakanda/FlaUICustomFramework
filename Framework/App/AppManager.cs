using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Framework.Utils;
using System;
using System.Diagnostics;
using System.Linq;

namespace Framework.App
{
    public class AppManager
    {
        private static Application _application = null;
        private readonly string _path;
        private UIA3Automation _automation;

        public AppManager(string path)
        {
            _path = path;
            _automation = new UIA3Automation();
        }

        public AppManager Launch()
        {

            if (_application == null)
            {
                Logger.Info("Launched application...");
                _application = Application.Launch(_path);

                if (_application == null || _application.HasExited)
                {
                    throw new InvalidOperationException("Failed to launch application.");
                }
                else
                {
                    Logger.Info($"Application launched with process ID: {_application.ProcessId}");
                }
            }
            return this;
        }

        public AppManager Attach(string processName)
        {
            var existingProcess = Retry.WhileNull(() => Process.GetProcessesByName(processName).FirstOrDefault()).Result;

            if (existingProcess != null)
            {
                Logger.Info($"Found existing process with ID: {existingProcess.Id}. Attaching to it.");
                _application = Application.Attach(existingProcess.Id);

                if (_application == null || _application.HasExited)
                {
                    throw new InvalidOperationException("Failed to attach to application.");
                }
            }
            else
            {
                throw new InvalidOperationException("No existing process found to attach.");
            }

            return this;
        }

        public WinManager GetWindowByName(string windowName)
        {
            Logger.Info($"Searching for window with name '{windowName}'...");

            var window = Retry.WhileNull(() => _automation.GetDesktop().FindFirstDescendant(cf => cf.ByName(windowName)).AsWindow(), TimeSpan.FromSeconds(20), TimeSpan.FromMilliseconds(500)).Result;
            if (window == null)
            {
                throw new InvalidOperationException($"Window with name '{windowName}' not found.");
            }

            Logger.Info($"Window with name '{windowName}' found.");
            return new WinManager(window);
        }

        public void CloseApplication()
        {
            try
            {
                if (_application != null && !_application.HasExited)
                {
                    Logger.Info("Closing application...");
                    _application.Close();
                    _application.WaitWhileBusy();
                    _application.Kill();
                }
                else
                {
                    Logger.Info("Application is already closed or not valid.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error while closing application: {ex.Message}");
            }
        }
    }
}
