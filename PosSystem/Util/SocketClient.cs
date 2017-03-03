using System;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using KidsPos.Object.Database;
using PosSystem.Object.Database;
using PosSystem.Object;

namespace PosSystem.Util
{
    public class SocketClient : SocketBase
    {
        static SocketClient instance = new SocketClient();
        public static SocketClient getInstance()
        {
            return instance;
        }

        TcpClient client = null;
        SocketClient() { }

        string ip;
        public bool ClientStart(string targetIP)
        {
            try
            {
                this.ip = targetIP;
                client = new TcpClient(targetIP, Config.getInstance().targetPort);
                thread = new Thread(new ThreadStart(this.ClientListen));
                thread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ClientListen()
        {
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[100];
            while (true)
            {
                try
                {
                    int intCount = stream.Read(bytes, 0, bytes.Length);
                    if (intCount > 0)
                    {
                        Byte[] getByte = new byte[intCount];
                        for (int i = 0; i < intCount; i++)
                            getByte[i] = bytes[i];

                        byte[] uniBytes;
                        uniBytes = Encoding.Convert(ecSjis, ecUni, bytes);
                        string strGetText = ecUni.GetString(uniBytes);
                        strGetText = strGetText.Substring(0, strGetText.IndexOf((char)0));
                        listener.onReceive(strGetText);
                    }
                    else
                    {
                        stream.Close();
                        stream = null;
                        Thread.Sleep(20);//これを入れないとNullReferenceExceptionが起きる
                        MessageBox.Show("サーバーから切断されました");
                        StopSock();
                    }
                }
                catch
                {
                    return;
                }
            }
        }
        public void StopSock(SocketCloseType type = SocketCloseType.CORRECT)
        {
            CloseClient(type);
        }
        private void CloseClient(SocketCloseType type)
        {
            try
            {
                client.GetStream().Close();
            }
            catch { }
            if (client != null && client.Connected)
            {
                client.Close();
                listener.onClose(type);
            }

            if (thread != null)
                thread.Abort();
        }
        public void sendData(string dataText)
        {
            Byte[] data = ecSjis.GetBytes(dataText);
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + Environment.NewLine + "送信できませんでした。", "送信エラー");
            }
        }
        public void registUser(StaffObject staff)
        {
            if (this.client != null) this.sendData(string.Format("staff,{0},{1}", staff.Name, staff.Barcode));
            new Database().insert<StaffObject>(staff);
        }
        public void RestartServer()
        {
            StopSock();
            ClientStart(this.ip);
        }
    }
}
