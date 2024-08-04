namespace Framework.Base
{
    public class BasePage
    {
        private BaseElements _uniqueItem;

        public BasePage(BaseElements uniqueItem)
        {
            _uniqueItem = uniqueItem;
        }

        public bool IsOpen() => _uniqueItem.IsExists();
    }
}
