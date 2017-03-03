namespace KidsPos.Object
{
    public class PrintItemObject
    {
        public BarcodeObject Barcode { get; private set; }
        public string ItemName { get; private set; }
        public string StoreName { get; private set; }
        public PrintItemObject(string barcode, string itemName, string storeName)
        {
            Barcode = new BarcodeObject(barcode);
            ItemName = itemName;
            StoreName = storeName;
        }
    }
}
