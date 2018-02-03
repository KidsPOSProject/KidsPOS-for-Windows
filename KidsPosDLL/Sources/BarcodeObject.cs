﻿using System;
using System.Drawing;
using System.IO;
using KidsPos.Sources.Setting;
using ZXing;

namespace KidsPos.Sources
{
    public class BarcodeObject
    {
        private readonly string _itemNum;
        private readonly string _store;

        /// <summary>
        ///     Systemバーコード, 生成済みバーコード
        /// </summary>
        /// <param name="barcode"></param>
        public BarcodeObject(string barcode)
        {
            if (barcode.Length == BarcodeConfig.Item.Length)
                barcode = BarcodeConfig.Prefix + barcode +
                          0.ToString("D" + (BarcodeConfig.BarcodeNum - BarcodeConfig.Prefix.Length -
                                            BarcodeConfig.Item.Length));
            else if (barcode.Length != BarcodeConfig.BarcodeNum)
                throw new InvalidDataException();
            Barcode = barcode;
        }

        public BarcodeObject(int storeNum, int itemNum)
        {
            _store = storeNum.ToString("D" + BarcodeConfig.DataMidLength);
            _itemNum = itemNum.ToString("D" + BarcodeConfig.DataLength);
            Gen();
        }

        public string Barcode { get; private set; }

        private void Gen()
        {
            var barcode = BarcodeConfig.Prefix + BarcodeConfig.Item + _store + _itemNum;
            if (barcode.Length == BarcodeConfig.BarcodeNum)
                Barcode = barcode;
            else
                throw new Exception("作られたバーコードの長さがおかしいなぁ・・" + Environment.NewLine + barcode);
        }

        public Bitmap GetBitmap()
        {
            var writer = new BarcodeWriter();
            writer.Format = BarcodeConfig.Format;
            return writer.Write("A" + Barcode + "A");
        }
    }
}