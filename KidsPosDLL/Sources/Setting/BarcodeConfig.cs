using ZXing;

namespace KidsPos.Setting
{
    public class BarcodeConfig
    {
        public const BarcodeFormat Format = BarcodeFormat.CODABAR;

        // 00 11 22 33 4444
        public const int BarcodeNum = 10;
        public const int PrefixLength = 4;
        public const int DataLength = 4;

        // USER: year([--**]), ITEM: storeNum
        public const int DataMidLength = BarcodeNum - PrefixLength - DataLength;

        // prefix
        public const string Prefix = "10";
        public const string Staff = "00";
        public const string Item = "01";
        public const string Sale = "02"; // レシート

        //画面遷移
        public const string ItemRegist = "17";
        public const string ItemList = "11";
        public const string SaleList = "12";
        public const string Account = "13";
        public const string StaffRegist = "14";
        public const string StaffList = "15";
        public const string ItemListEdit = "16";

        //操作
        public const string Enter = "20";
        public const string Back = "21";
        public const string ChangeVisibleDebugToolbar = "22";
        public const string ChangeVisibleToolbar = "23";

        //動作モード変更
        public const string ModeTake = "30";
        public const string ModePractice = "31";
    }
}