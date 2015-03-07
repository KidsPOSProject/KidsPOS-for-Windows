using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using PosSystem.Setting;
using ZXing;

namespace PosSystem.Setting
{
    public class BarcodeConfig
    {
        public const BarcodeFormat format = BarcodeFormat.CODABAR;

        // 00 11 22 33 4444
        public const int BARCODE_NUM = 10;
        public const int PREFIX_LENGTH = 4;
        public const int DATA_LENGTH = 4;

        // USER: year([--**]), ITEM: storeNum
        public const int DATA_MID_LENGTH = BARCODE_NUM - PREFIX_LENGTH - DATA_LENGTH;

        // prefix
        public const string PREFIX = "10";
        public const string STAFF = "00";
        public const string ITEM = "01";
        public const string SALE = "02"; // レシート

        //画面遷移
        public const string ITEM_REGIST = "17";
        public const string ITEM_LIST = "11";
        public const string SALE_LIST = "12";
        public const string ACCOUNT = "13";
        public const string STAFF_REGIST = "14";
        public const string STAFF_LIST = "15";
        public const string ITEM_LIST_EDIT = "16";

        //操作
        public const string ENTER = "20";
        public const string BACK = "21";
        public const string CHANGE_VISIBLE_DEBUG_TOOLBAR = "22";
        public const string CHANGE_VISIBLE_TOOLBAR = "23";

        //動作モード変更
        public const string MODE_TAKE = "30";
        public const string MODE_PRACTICE = "31";
    }
}
