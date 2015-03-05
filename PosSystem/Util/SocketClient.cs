using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using PosSystem.Setting;
using PosSystem.Object.Database;

namespace PosSystem.Util
{
    public class SocketClient : SocketBase
    {
        TcpClient client = null;
        public SocketClient(SocketListener listener): base(listener){}

        public bool ClientStart()
        {
            try
            {
                client = new TcpClient(PosInformation.getInstance().targetIP, PosInformation.port);
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
                        listener.onReceiveDel(strGetText);
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
            NetworkStream ns = client.GetStream();
            if (client != null && client.Connected)
                ns.Close();
            client.Close();

            if (thread != null)
                thread.Abort();
            listener.onConnectCloseDel(type);
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
            if (this.client != null) this.sendData(string.Format("staff,{0},{1}", staff.name, staff.barcode));
            new Database().insert<StaffObject>(staff);
        }
        public void RestartServer()
        {
            StopSock();
            ClientStart();
        }
    }
}
