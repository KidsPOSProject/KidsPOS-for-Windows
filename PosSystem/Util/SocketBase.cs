using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PosSystem.Util
{
    public class SocketBase
    {
        public Encoding ecUni = Encoding.GetEncoding("utf-16");
        public Encoding ecSjis = Encoding.GetEncoding("shift-jis");
        public Thread thread = null;

        public SocketListener listener = null;
        public SocketBase(SocketListener listener)
        {
            this.listener = listener;
        }
    }
    public class SocketListener
    {
        /// <summary>
        /// データを受信したときの処理 受信例: reveice,barcode,1000150001,山田
        /// </summary>
        /// <param name="text"></param>
        public delegate void onReceive(string receiveText);
        public delegate void onConnectClose(SocketCloseType closeType);
        public onReceive onReceiveDel;
        public onConnectClose onConnectCloseDel;
    }
    public enum SocketCloseType
    {
        ERROR,
        CORRECT
    }
}
