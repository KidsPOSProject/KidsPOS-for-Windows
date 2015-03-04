using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PosSystem.Object;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PosSystem.Setting;

namespace PosSystem.Util
{
    public class Print
    {
        static Print instance = new Print();
        Print(){
        }
        public static Print getInstance() { return instance; }
        public void printItemBarcode(PrintItemObject obj, bool drawGrid, PrintPageEventArgs e)
        {
            // 縦横の印刷数
            int ROW_NUM = 4;
            int COL_NUM = 11;

            //ミリメートルで指定

            //全体の余白
            float MARGIN_PAGE_TOP = 8f;
            float MARGIN_PAGE_LEFT = 8.4f;

            //一つ一つのサイズ
            float MARGIN_PRINT_HEIGHT = 25.4f;
            float MARGIN_PRINT_WEIGHT = 48.3f;


            Graphics graphics = e.Graphics;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            // グリッド線を印刷のチェックが入っている時
            if (drawGrid)
            {
                //線引いてみる
                for (int j = 0; j < ROW_NUM + 1; j++)
                {
                    for (int i = 0; i < COL_NUM + 1; i++)
                    {
                        float hei = i * MARGIN_PRINT_HEIGHT;
                        float wei = j * MARGIN_PRINT_WEIGHT;
                        // 線の太さ
                        float penSize = 0.1f;
                        //横の線
                        graphics.DrawLine(new Pen(Brushes.Black, penSize),
                            new Point((int)MARGIN_PAGE_LEFT, (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * i))),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * ROW_NUM)), (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * i))));

                        //縦の線
                        graphics.DrawLine(new Pen(Brushes.Black, penSize),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * j)), (int)(MARGIN_PAGE_TOP)),
                            new Point((int)(MARGIN_PAGE_LEFT + (MARGIN_PRINT_WEIGHT * j)), (int)(MARGIN_PAGE_TOP + (MARGIN_PRINT_HEIGHT * COL_NUM))));
                    }
                }
            }

            Bitmap barcode = obj.barcode.getBitmap();
            //バーコードの印刷
            for (int j = 0; j < ROW_NUM; j++)
            {
                for (int i = 0; i < COL_NUM; i++)
                {
                    float hei = i * MARGIN_PRINT_HEIGHT;
                    float wei = j * MARGIN_PRINT_WEIGHT;
                    graphics.DrawImage(
                        barcode,
                        MARGIN_PAGE_LEFT + wei + 3.2f,
                        MARGIN_PAGE_TOP + hei + 7f,
                        barcode.Width * 0.3f, barcode.Height * 0.14f
                        );

                    graphics.DrawString(
                        obj.itemName,
                        new Font("MS UI Gothic", 9),
                        Brushes.Black,
                        new PointF(
                            MARGIN_PAGE_LEFT + wei + 1f,
                            MARGIN_PAGE_TOP + hei + 0.2f
                            )
                        );

                    graphics.DrawString(
                        "おみせ: " + obj.storeName,
                        new Font("MS UI Gothic", 8), Brushes.Black,
                        new PointF(
                            MARGIN_PAGE_LEFT + wei + 1f,
                            MARGIN_PAGE_TOP + hei + 3.5f
                            )
                        );
                }
            }
        }
        public void printReceipt(ListView _item_list, string _deposit, PrintPageEventArgs e, string ACCOUNT_CODE)
        {
            int marginMin = 3;
            int marginMax = 70;
            int alignCenter = 27;
            int lineHeight = 7;

            int drawHeightPosition = 0;

            Graphics graphics = e.Graphics;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            Font font = new Font("MS UI Gothic", 10);
            Font fontBig = new Font("MS UI Gothic", 13);

            graphics.DrawImage(Image.FromFile(@"Kids.jpg"), 3, 3, 67, 20);
            drawHeightPosition += lineHeight + 22;

            drawString(graphics, fontBig, "<レシート>", alignCenter, drawHeightPosition);
            drawHeightPosition += lineHeight + 3;

            drawString(graphics, font, DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒"),
                marginMin,
                drawHeightPosition);
            drawHeightPosition += lineHeight;

            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));
            drawHeightPosition += lineHeight;

            for (int i = 0; i < _item_list.Items.Count; i++)
            {
                ListViewItem lvi = _item_list.Items[i];
                drawString(graphics, fontBig, lvi.SubItems[0].Text + "  " + lvi.SubItems[1].Text, marginMin, drawHeightPosition);
                drawString(graphics, fontBig, "\t\t\\" + lvi.SubItems[3].Text, marginMin + 15, drawHeightPosition);
                drawHeightPosition += lineHeight;
            }
            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));
            drawHeightPosition += lineHeight;

            int sum = 0;
            foreach (ListViewItem v in _item_list.Items)
                sum += int.Parse(v.SubItems[3].Text);

            drawString(graphics, fontBig, "ごうけい", marginMin, drawHeightPosition);
            drawString(graphics, fontBig, "\t\t\\" + sum.ToString(), marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;

            drawString(graphics, fontBig, "おあずかり", marginMin, drawHeightPosition);
            drawString(graphics, fontBig, "\t\t\\" + _deposit, marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;

            drawString(graphics, fontBig, "おつり", marginMin, drawHeightPosition);
            drawString(graphics, fontBig, "\t\t\\" + (int.Parse(_deposit) - sum).ToString(), marginMin + 15, drawHeightPosition);
            drawHeightPosition += lineHeight;
            drawHeightPosition += lineHeight;

            drawString(graphics, fontBig, "おみせ：　" + PosInformation.storeName, marginMin, drawHeightPosition);
            drawHeightPosition += lineHeight;

            drawString(graphics, fontBig, "れじのたんとう：　" + PosInformation.regUserName, marginMin, drawHeightPosition);
            drawHeightPosition += lineHeight + 5;


            drawString(graphics, fontBig,
                "印字保護のためこちらの面を" + Environment.NewLine +
                "内側に折って保管してください", marginMin + 3, drawHeightPosition);
            drawHeightPosition += lineHeight;
            drawHeightPosition += lineHeight;

            BarcodeObject barcode = new BarcodeObject(ACCOUNT_CODE);

            graphics.DrawImage(barcode.getBitmap(), new Point(alignCenter - 5, drawHeightPosition));
            drawHeightPosition += lineHeight + 30;

            graphics.DrawLine(new Pen(Brushes.Black),
                new Point(marginMin, drawHeightPosition),
                new Point(marginMax, drawHeightPosition));

            e.HasMorePages = false;
        }
        private void drawString(Graphics g, Font f, string s, int x, int y)
        {
            g.DrawString(s, f, Brushes.Black, new PointF(x, y));
        }
    }
}
