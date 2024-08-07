using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using Framework.Base;

namespace Framework.UIElementWrappers
{
    public class QElement : BaseElements
    {
        public QElement(AutomationElement rootElement, ConditionBase elementLocator, string elementName)
            : base(rootElement, elementLocator, elementName)
        {
        }
    }
}
