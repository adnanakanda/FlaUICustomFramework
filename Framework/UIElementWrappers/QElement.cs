using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using Framework.Base;
using Framework.FileUtils;
using System;

namespace Framework.UIElementWrappers
{
    public class QElement : BaseElements
    {
        public QElement(AutomationElement rootElement, ConditionBase elementLocator, string elementName)
            : base(rootElement, elementLocator, elementName)
        {
        }

        public string GetText()
        {
            var element = GetElement();
            var label = element.AsLabel();
            if (label == null)
            {
                Logger.Error($"Element found is not a Label: {_elementName}");
                throw new InvalidCastException($"Element found is not a Label: {_elementName}");
            }
            return label.Text;
        }
    }
}
