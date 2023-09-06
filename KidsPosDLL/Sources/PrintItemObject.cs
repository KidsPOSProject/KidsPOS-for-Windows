namespace KidsPos.Object
{
    public class PrintItemObject
    {
        public PrintItemObject(string barcode, string itemName, string storeName)
        {
            Barcode = new BarcodeObject(barcode);
            ItemName = itemName;
            StoreName = storeName;
        }

        public BarcodeObject Barcode { get; private set; }
        public string ItemName { get; private set; }
        public string StoreName { get; private set; }
    }
}