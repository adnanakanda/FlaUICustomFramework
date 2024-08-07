using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Tools;
using Framework.Utils;
using System;

namespace Framework.Base
{
    public abstract class BaseElements
    {
        protected ConditionBase _elementLocator;
        protected string _elementName;
        protected AutomationElement _rootElement;

        protected BaseElements(AutomationElement rootElement, ConditionBase elementLocator, string elementName)
        {
            _rootElement = rootElement;
            _elementLocator = elementLocator;
            _elementName = elementName;
        }

        public bool IsExists()
        {
            try
            {
                Logger.Info($"Checking existence of {_elementName}...");
                var element = GetElement();
                bool exists = element != null;
                Logger.Info($"{_elementName} existence check result: {exists}");
                return exists;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error while checking existence of {_elementName}: {ex.Message}");
                return false;
            }
        }

        public void Click()
        {
            Logger.Info($"Clicking on {_elementName}");
            var element = GetElement();
            if (element != null)
            {
                element.Click();
                Logger.Info($"Clicked on {_elementName}");
            }
            else
            {
                Logger.Error($"Cannot click on {_elementName} because it does not exist.");
            }
        }

        protected AutomationElement GetElement()
        {
            try
            {
                Logger.Info($"Element Retrived: {_elementName}");
                var element = Retry.WhileNull(() => _rootElement.FindFirstDescendant(_elementLocator), TimeSpan.FromSeconds(10)).Result;
                if (element != null)
                {
                    Logger.Info($"Retrieved element: {_elementName}");
                }
                else
                {
                    Logger.Error($"Element not found: {_elementName}");
                }
                return element;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error while retrieving element {_elementName}: {ex.Message}");
                return null;
            }
        }
    }
}
