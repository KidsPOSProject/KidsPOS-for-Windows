using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PosSystem.Util
{
    public class SocketBase
    {
        public Encoding ecUni = Encoding.GetEncoding("utf-16");
        public Encoding ecSjis = Encoding.GetEncoding("shift-jis");
        public Thread thread = null;

        public SocketListener listener = null;
        public SocketBase()
        {
        }
        public void init(SocketListener listener)
        {
            this.listener = listener;
        }
    }
    public enum SocketCloseType
    {
        ERROR,
        CORRECT
    }
    abstract public class SocketListener
    {
        Form context;
        delegate void receiveDelegate(string json);
        delegate void closeDelegate(SocketCloseType closeType);
        public SocketListener(Form context)
        {
            this.context = context;
        }
        public void receive(string text)
        {
            context.BeginInvoke(new receiveDelegate(this.onReceive),text);
        }
        public void close(SocketCloseType closeType)
        {
            context.BeginInvoke(new closeDelegate(this.onClose), closeType);
        }
        public Form getContext()
        {
            return this.context;
        }
        /// <summary>
        /// データを受信したときの処理 受信例: reveice,barcode,1000150001,山田
        /// </summary>
        /// <param name="text"></param>
        abstract public void onReceive(string text);
        abstract public void onClose(SocketCloseType closeType);
    }
}
