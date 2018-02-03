using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KidsPos.Sources.Util
{
    public class SocketBase
    {
        public Encoding EcSjis = Encoding.GetEncoding("shift-jis");
        public Encoding EcUni = Encoding.GetEncoding("utf-16");

        public SocketListener Listener;
        public Thread Thread = null;

        public void Init(SocketListener listener)
        {
            Listener = listener;
        }
    }

    public enum SocketCloseType
    {
        Error,
        Correct
    }

    public abstract class SocketListener
    {
        private readonly Form _context;

        protected SocketListener(Form context)
        {
            _context = context;
        }

        public void Receive(string text)
        {
            _context.BeginInvoke(new ReceiveDelegate(OnReceive), text);
        }

        public void Close(SocketCloseType closeType)
        {
            _context.BeginInvoke(new CloseDelegate(OnClose), closeType);
        }

        public Form GetContext()
        {
            return _context;
        }

        /// <summary>
        ///     データを受信したときの処理 受信例: reveice,barcode,1000150001,山田
        /// </summary>
        /// <param name="text"></param>
        public abstract void OnReceive(string text);

        public abstract void OnClose(SocketCloseType closeType);

        private delegate void ReceiveDelegate(string json);

        private delegate void CloseDelegate(SocketCloseType closeType);
    }
}