using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using Framework.Base;
using Framework.Utils;
using System;
using System.Threading;

namespace Framework.UIElementWrappers
{
    public class QButton : BaseElements
    {
        public QButton(AutomationElement rootElement, ConditionBase elementLocator, string elementName)
            : base(rootElement, elementLocator, elementName)
        {
        }

        public void Invoke(int delayInMilliseconds = 0, bool highlight = false)
        {
            try
            {
                if (delayInMilliseconds > 0)
                {
                    Logger.Info($"Waiting for {delayInMilliseconds} milliseconds after invoking button '{_elementName}'");
                    Thread.Sleep(delayInMilliseconds);
                }

                var button = GetElement();
                if (button == null)
                {
                    throw new InvalidOperationException($"Button '{_elementName}' not found.");
                }

                if (highlight)
                {
                    Logger.Info($"Highlighting button: {_elementName}");
                    button.DrawHighlight();
                }

                if (button.Patterns.Invoke.IsSupported)
                {
                    Logger.Info($"Invoking button: {_elementName}");
                    button.Patterns.Invoke.Pattern.Invoke();
                }
                else if (button.Patterns.LegacyIAccessible.IsSupported)
                {
                    Logger.Info($"Invoking button via LegacyIAccessible: {_elementName}");
                    button.Patterns.LegacyIAccessible.Pattern.DoDefaultAction();
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
