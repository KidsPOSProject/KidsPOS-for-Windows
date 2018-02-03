namespace KidsPos.Sources
{
    public class PrintItemObject
    {
        public PrintItemObject(string barcode, string itemName, string storeName)
        {
            Barcode = new BarcodeObject(barcode);
            ItemName = itemName;
            StoreName = storeName;
        }

        public BarcodeObject Barcode { get; }
        public string ItemName { get; }
        public string StoreName { get; }
    }
}