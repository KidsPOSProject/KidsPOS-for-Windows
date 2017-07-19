using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Management;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;
using KidsPos.Setting;

namespace KidsPos.Util
{
    public class Print
    {
        private static readonly Print Instance = new Print();
        private Print(){}
        public static Print GetInstance() { return Instance; }
        public void PrintItemBarcode(PrintItemObject obj, bool drawGrid, PrintPageEventArgs e)
        {
            // 縦横の印刷数
            const int rowNum = 4;
            const int colNum = 11;

            //ミリメートルで指定

            //全体の余白
            const float marginPageTop = 8f;
            const float marginPageLeft = 8.4f;

            //一つ一つのサイズ
            const float marginPrintHeight = 25.4f;
            const float marginPrintWeight = 48.3f;

            var graphics = e.Graphics;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            // グリッド線を印刷のチェックが入っている時
            if (drawGrid)
            {
                //線引いてみる
                for (var j = 0; j < rowNum + 1; j++)
                {
                    for (var i = 0; i < colNum + 1; i++)
                    {
                        // 線の太さ
                        var penSize = 0.1f;
                        //横の線
                        graphics.DrawLine(new Pen(Brushes.Black, penSize),
                            new Point((int)marginPageLeft, (int)(marginPageTop + marginPrintHeight * i)),
                            new Point((int)(marginPageLeft + (marginPrintWeight * rowNum)), (int)(marginPageTop + (marginPrintHeight * i))));

                        //縦の線
                        graphics.DrawLine(new Pen(Brushes.Black, penSize),
                            new Point((int)(marginPageLeft + (marginPrintWeight * j)), (int)marginPageTop),
                            new Point((int)(marginPageLeft + (marginPrintWeight * j)), (int)(marginPageTop + (marginPrintHeight * colNum))));
                    }
                }
            }

            var barcode = obj.Barcode.GetBitmap();
            //バーコードの印刷
            for (var j = 0; j < rowNum; j++)
            {
                for (var i = 0; i < colNum; i++)
                {
                    var hei = i * marginPrintHeight;
                    var wei = j * marginPrintWeight;
                    graphics.DrawImage(
                        barcode,
                        marginPageLeft + wei + 3.2f,
                        marginPageTop + hei + 7f,
                        barcode.Width * 0.3f, barcode.Height * 0.14f
                        );

                    graphics.DrawString(
                        obj.ItemName,
                        new Font("MS UI Gothic", 9),
                        Brushes.Black,
                        new PointF(
                            marginPageLeft + wei + 1f,
                            marginPageTop + hei + 0.2f
                            )
                        );

                    graphics.DrawString(
                        "おみせ: " + obj.StoreName,
                        new Font("MS UI Gothic", 8), Brushes.Black,
                        new PointF(
                            marginPageLeft + wei + 1f,
                            marginPageTop + hei + 3.5f
                            )
                        );
                }
            }
        }
        public void PrintReceipt(ListView itemList, string deposit, PrintPageEventArgs e, string accountCode)
        {
            var marginMin = 3;
            var marginMax = 70;
            var alignCenter = 27;
            var lineHeight = 7;

            var drawHeightPosition = 0;

            var graphics = e.Graphics;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            var font = new Font("MS UI Gothic", 10);
            var fontBig = new Font("MS UI Gothic", 13);

            graphics.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);
            drawHeightPosition += lineHeight + 22;

            DrawString(graphics, fontBig, "<レシート>", alignCenter, drawHeightPosition);
            drawHeightPosition += lineHeight + 3;

            DrawString(graphics, font, DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒"),
                marginMin,
                drawHeightPosition);
            drawHeightPosition += lineHeight;

            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));
            drawHeightPosition += lineHeight;

            for (var i = 0; i < itemList.Items.Count; i++)
            {
                var lvi = itemList.Items[i];
                DrawString(graphics, fontBig, lvi.SubItems[0].Text + "  " + lvi.SubItems[1].Text, marginMin, drawHeightPosition);
                DrawString(graphics, fontBig, "\t\t\\" + lvi.SubItems[3].Text, marginMin + 15, drawHeightPosition);
                drawHeightPosition += lineHeight;
            }
            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));
            drawHeightPosition += lineHeight;

            var sum = 0;
            foreach (ListViewItem v in itemList.Items)
                sum += int.Parse(v.SubItems[3].Text);

            DrawString(graphics, fontBig, "ごうけい", marginMin, drawHeightPosition);
            DrawString(graphics, fontBig, "\t\t\\" + sum, marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;

            DrawString(graphics, fontBig, "おあずかり", marginMin, drawHeightPosition);
            DrawString(graphics, fontBig, "\t\t\\" + deposit, marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;

            DrawString(graphics, fontBig, "おつり", marginMin, drawHeightPosition);
            DrawString(graphics, fontBig, "\t\t\\" + (int.Parse(deposit) - sum), marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;
            drawHeightPosition += lineHeight;

            DrawString(graphics, fontBig, "おみせ：　" + Config.GetInstance().Store.Name, marginMin, drawHeightPosition);
            drawHeightPosition += lineHeight;

            DrawString(graphics, fontBig, "れじのたんとう：　" + PosInformation.GetInstance().GetStaffName(), marginMin, drawHeightPosition);
            drawHeightPosition += lineHeight + 5;


            DrawString(graphics, fontBig,
                "印字保護のためこちらの面を" + Environment.NewLine +
                "内側に折って保管してください", marginMin + 3, drawHeightPosition);
            drawHeightPosition += lineHeight;
            drawHeightPosition += lineHeight;

            var barcode = new BarcodeObject(accountCode).GetBitmap();

            graphics.DrawImage(barcode, alignCenter - 13, drawHeightPosition, barcode.Width * 0.34f, barcode.Height * 0.14f);
            drawHeightPosition += lineHeight + 10;

            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));

            e.HasMorePages = false;
        }
        private class PrintConfigSystemBarcode
        {
            public int DrawHeightPosition;
            public readonly Graphics Graphics;
            public readonly int MarginMin = 3;
            public readonly int MarginMax = 70;
            public readonly int AlignCenter = 27;
            public readonly int LineHeight = 7;
            public readonly Font Font = new Font("MS UI Gothic", 10);
            public readonly Font FontBig = new Font("MS UI Gothic", 13);

            public PrintConfigSystemBarcode(Graphics g)
            {
                Graphics = g;
                g.PageUnit = GraphicsUnit.Millimeter;
                g.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);
            }
        }
        public void PrintSystemBarcode(object sender, PrintPageEventArgs e)
        {
            var config = new PrintConfigSystemBarcode(e.Graphics);

            config.DrawHeightPosition += config.LineHeight + 22;

            DrawString(config.Graphics, config.FontBig, "<システムバーコード>", config.AlignCenter - 20, config.DrawHeightPosition);

            config.DrawHeightPosition += config.LineHeight + 3;

            config.Graphics.DrawLine(new Pen(Brushes.Black),
                new Point(config.MarginMin, config.DrawHeightPosition),
                new Point(config.MarginMax, config.DrawHeightPosition));

            config.DrawHeightPosition += config.LineHeight + 2;

            /* ---  バーコード生成  --- */

            DrawBarcode("かいけい", BarcodeConfig.Account, ref config);
            DrawBarcode("商品リスト", BarcodeConfig.ItemList, ref config);
            DrawBarcode("売上リスト", BarcodeConfig.SaleList, ref config);
            DrawBarcode("ツールバー表示切り替え", BarcodeConfig.ChangeVisibleToolbar, ref config);
            DrawBarcode("デバッグ用ツールバー表示切り替え", BarcodeConfig.ChangeVisibleDebugToolbar, ref config);

            config.Graphics.DrawLine(new Pen(Brushes.Black),
                new Point(config.MarginMin, config.DrawHeightPosition),
                new Point(config.MarginMax, config.DrawHeightPosition));

            e.HasMorePages = false;
        }
        public void PrintDummyUserBarcode(object sender, PrintPageEventArgs e)
        {
            var config = new PrintConfigSystemBarcode(e.Graphics);

            config.DrawHeightPosition += config.LineHeight + 22;

            DrawString(config.Graphics, config.FontBig, "<ダミーユーザ>", config.AlignCenter - 20, config.DrawHeightPosition);

            config.DrawHeightPosition += config.LineHeight + 3;

            config.Graphics.DrawLine(new Pen(Brushes.Black),
                new Point(config.MarginMin, config.DrawHeightPosition),
                new Point(config.MarginMax, config.DrawHeightPosition));

            config.DrawHeightPosition += config.LineHeight + 2;

            /* ---  バーコード生成  --- */

            DrawBarcode("ダミー", new StaffObject(9999, "").Barcode, ref config);

            config.Graphics.DrawLine(new Pen(Brushes.Black),
                new Point(config.MarginMin, config.DrawHeightPosition),
                new Point(config.MarginMax, config.DrawHeightPosition));

            e.HasMorePages = false;
        }

        private void DrawString(Graphics g, Font f, string s, int x, int y)
        {
            g.DrawString(s, f, Brushes.Black, new PointF(x, y));
        }
        private void DrawBarcode(
            string sysName, string sysCode, ref PrintConfigSystemBarcode c)
        {
            DrawString(c.Graphics, c.Font, sysName, c.AlignCenter - 12, c.DrawHeightPosition);
            c.DrawHeightPosition += c.LineHeight - 3;

            var barcode = new BarcodeObject(sysCode).GetBitmap();
            c.Graphics.DrawImage(barcode, c.AlignCenter - 13, c.DrawHeightPosition, barcode.Width * 0.34f, barcode.Height * 0.14f);
            c.DrawHeightPosition += c.LineHeight + 10;
        }
        public static bool PingToPrinter(string printerIp)
        {
            var mo = new ManagementObject($"Win32_PingStatus.address='{printerIp}'");
            var ret = (uint)mo.Properties["StatusCode"].Value == 0;
            mo.Dispose();
            return ret;
        }
    }
}
