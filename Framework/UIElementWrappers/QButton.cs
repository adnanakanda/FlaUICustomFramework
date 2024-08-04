using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using Framework.Base;
using Framework.FileUtils;
using System;

namespace Framework.UIElementWrappers
{
    public class QButton : BaseElements
    {
        public QButton(AutomationElement rootElement, ConditionBase elementLocator, string elementName)
            : base(rootElement, elementLocator, elementName)
        {
        }

        public void Invoke()
        {
            try
            {
                var button = GetElement();
                if (button == null)
                {
                    throw new InvalidOperationException($"Button '{_elementName}' not found.");
                }

                if (button.Patterns.Invoke.IsSupported)
                {
                    var invokePattern = button.Patterns.Invoke.Pattern;
                    Logger.Info($"Invoking button: {_elementName}");
                    invokePattern.Invoke();
                }
                else if (button.Patterns.LegacyIAccessible.IsSupported)
                {
                    var legacyPattern = button.Patterns.LegacyIAccessible.Pattern;
                    Logger.Info($"Invoking button via LegacyIAccessible: {_elementName}");
                    legacyPattern.DoDefaultAction();
                }
                else if (button.Patterns.SelectionItem.IsSupported)
                {
                    var selectionItemPattern = button.Patterns.SelectionItem.Pattern;
                    Logger.Info($"Selecting button: {_elementName}");
                    selectionItemPattern.Select();
                }
                else
                {
                    throw new InvalidOperationException($"Button '{_elementName}' does not support any known invoke patterns.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error invoking button '{_elementName}': {ex.Message}");
                throw;
            }
        }
    }
}
