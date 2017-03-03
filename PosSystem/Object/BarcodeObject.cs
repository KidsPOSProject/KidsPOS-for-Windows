using System;
using System.Drawing;
using System.IO;
using KidsPos.Setting;
using ZXing;

namespace KidsPos.Object
{
    public class BarcodeObject
    {
        private readonly string _store;
        private readonly string _itemNum;
        public string Barcode { get; private set; }
        /// <summary>
        /// Systemバーコード, 生成済みバーコード
        /// </summary>
        /// <param name="barcode"></param>
        public BarcodeObject(string barcode)
        {
            if (barcode.Length == BarcodeConfig.Item.Length)
            {
                barcode = BarcodeConfig.Prefix + barcode + 0.ToString("D" + (BarcodeConfig.BarcodeNum - BarcodeConfig.Prefix.Length - BarcodeConfig.Item.Length));
            }
            else if (barcode.Length != BarcodeConfig.BarcodeNum)
            {
                throw new InvalidDataException();
            }
            Barcode = barcode;
        }

        public BarcodeObject(int storeNum, int itemNum)
        {
            _store = storeNum.ToString("D" + BarcodeConfig.DataMidLength);
            _itemNum = itemNum.ToString("D" + BarcodeConfig.DataLength);
            Gen();
        }
        private void Gen()
        {
            var barcode = BarcodeConfig.Prefix + BarcodeConfig.Item + _store + _itemNum;
            if (barcode.Length == BarcodeConfig.BarcodeNum)
            {
                Barcode = barcode;
            }
            else
            {
                throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine + barcode);
            }
        }
        public Bitmap GetBitmap()
        {
            var writer = new BarcodeWriter();
            writer.Format = BarcodeConfig.Format;
            return writer.Write("A" + Barcode + "A");
        }
    }
}
