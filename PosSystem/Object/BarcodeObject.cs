using System;
using ZXing;
using System.Drawing;
using System.IO;
using PosSystem.Setting;

namespace PosSystem.Object
{
    public class BarcodeObject
    {
        BarcodeFormat format = BarcodeFormat.CODABAR;
        string store;
        string itemNum;
        public string barcode { get; private set; }
        public BarcodeObject(string barcode)
        {
            if (barcode.Length != Barcode.BARCODE_NUM) throw new InvalidDataException();
            this.barcode = barcode;
        }

        public BarcodeObject(int storeNum, int itemNum)
        {
            this.store = storeNum.ToString("D" + Barcode.DATA_MID_LENGTH);
            this.itemNum = itemNum.ToString("D" + Barcode.DATA_LENGTH);
            this.gen();
        }
        private void gen()
        {
            string barcode = Barcode.PREFIX + Barcode.ITEM + store + itemNum;
            if (barcode.Length == Barcode.BARCODE_NUM)
            {
                this.barcode = barcode;
            }
            else
            {
                throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine + barcode);
            }
        }
        public Bitmap getBitmap()
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = this.format;
            return writer.Write("A" + this.barcode + "A");
        }
    }
}
