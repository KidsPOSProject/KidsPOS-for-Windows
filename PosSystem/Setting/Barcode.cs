using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using PosSystem.Setting;
using ZXing;

namespace PosSystem.Setting
{
    public class Barcode
    {
        BarcodeFormat format = BarcodeFormat.CODABAR;

        // 00 11 22 33 4444
        public const int BARCODE_NUM = 10;
        public const int PREFIX_LENGTH = 4;
        public const int DATA_LENGTH = 4;

        // USER: year([--**]), ITEM: storeNum
        public const int DATA_MID_LENGTH = BARCODE_NUM - PREFIX_LENGTH - DATA_LENGTH;

        // prefix
        public const string PREFIX = "10";
        public const string USER = "00";
        public const string ITEM = "01";

    }
}
