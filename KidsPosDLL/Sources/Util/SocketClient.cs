using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using KidsPos.Object;
using KidsPos.Object.Database;

namespace KidsPos.Util
{
    public class SocketClient : SocketBase
    {
        private static readonly SocketClient Instance = new SocketClient();

        private TcpClient _client;

        private string _ip;

        private SocketClient()
        {
        }

        public static SocketClient GetInstance()
        {
            return Instance;
        }

        public bool ClientStart(string targetIp)
        {
            try
            {
                _ip = targetIp;
                _client = new TcpClient(targetIp, Config.GetInstance().TargetPort);
                Thread = new Thread(ClientListen);
                Thread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ClientListen()
        {
            var stream = _client.GetStream();
            var bytes = new byte[100];
            while (true)
                try
                {
                    if (stream != null)
                    {
                        var intCount = stream.Read(bytes, 0, bytes.Length);
                        if (intCount > 0)
                        {
                            var getByte = new byte[intCount];
                            for (var i = 0; i < intCount; i++)
                                getByte[i] = bytes[i];

                            var uniBytes = Encoding.Convert(EcSjis, EcUni, bytes);
                            var strGetText = EcUni.GetString(uniBytes);
                            strGetText = strGetText.Substring(0, strGetText.IndexOf((char)0));
                            Listener.OnReceive(strGetText);
                        }
                        else
                        {
                            stream.Close();
                            stream = null;
                            Thread.Sleep(20); //これを入れないとNullReferenceExceptionが起きる
                            MessageBox.Show(@"何らかの原因で登録できませんでした。");
                            StopSock();
                        }
                    }
                }
                catch
                {
                    return;
                }
        }

        public void StopSock(SocketCloseType type = SocketCloseType.Correct)
        {
            CloseClient(type);
        }

        private void CloseClient(SocketCloseType type)
        {
            try
            {
                _client.GetStream().Close();
            }
            catch
            {
                // ignored
            }

            if (_client != null && _client.Connected)
            {
                _client.Close();
                Listener.OnClose(type);
            }

            Thread?.Abort();
        }

        public void SendData(string dataText)
        {
            var data = EcSjis.GetBytes(dataText);
            try
            {
                var stream = _client.GetStream();
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e + Environment.NewLine + "送信できませんでした。", "送信エラー");
            }
        }

        public void RegistUser(StaffObject staff)
        {
            if (_client != null) SendData($"staff,{staff.Name},{staff.Barcode}");
            new Database().Insert(staff);
        }

        public void RestartServer()
        {
            StopSock();
            ClientStart(_ip);
        }
    }
}