using System;
using ZXing;
using System.Drawing;
using System.IO;
using PosSystem.Setting;
using System.Text;

namespace PosSystem.Object
{
    public class BarcodeObject
    {
        string store;
        string itemNum;
        public string barcode { get; private set; }
        /// <summary>
        /// Systemバーコード, 生成済みバーコード
        /// </summary>
        /// <param name="barcode"></param>
        public BarcodeObject(string barcode)
        {
            if (barcode.Length == BarcodeConfig.ITEM.Length)
            {
                barcode = BarcodeConfig.PREFIX + barcode + 0.ToString("D" + (BarcodeConfig.BARCODE_NUM - BarcodeConfig.PREFIX.Length - BarcodeConfig.ITEM.Length));
            }
            else if (barcode.Length != BarcodeConfig.BARCODE_NUM)
            {
                throw new InvalidDataException();
            }
            this.barcode = barcode;
        }

        public BarcodeObject(int storeNum, int itemNum)
        {
            this.store = storeNum.ToString("D" + BarcodeConfig.DATA_MID_LENGTH);
            this.itemNum = itemNum.ToString("D" + BarcodeConfig.DATA_LENGTH);
            this.gen();
        }
        private void gen()
        {
            string barcode = BarcodeConfig.PREFIX + BarcodeConfig.ITEM + store + itemNum;
            if (barcode.Length == BarcodeConfig.BARCODE_NUM)
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
            writer.Format = BarcodeConfig.format;
            return writer.Write("A" + this.barcode + "A");
        }
    }
}
