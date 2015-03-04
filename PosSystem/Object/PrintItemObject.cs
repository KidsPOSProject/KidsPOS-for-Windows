using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using PosSystem.Setting;

namespace PosSystem.Object
{
    public class PrintItemObject
    {
        public BarcodeObject barcode { get; private set; }
        public string itemName { get; private set; }
        public string storeName { get; private set; }
        public PrintItemObject(string barcode, string itemName, string storeName)
        {
            this.barcode = new BarcodeObject(barcode);
            this.itemName = itemName;
            this.storeName = storeName;
        }
    }
}
