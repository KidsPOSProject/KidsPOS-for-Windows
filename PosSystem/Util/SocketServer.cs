using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using PosSystem.Setting;
using System.Windows.Forms;
using PosSystem.Object;

namespace PosSystem.Util
{
    public class SocketServer :SocketBase
    {
        static SocketServer instance = new SocketServer();
        public static SocketServer getInstance()
        {
            return instance;
        }

        TcpListener tcpClient = null;
        List<ClientHandler> hClient = new List<ClientHandler>();
        SocketServer() { }

        public bool ServerStart()
        {
            try
            {
                thread = new Thread(new ThreadStart(this.ServerListen));
                thread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void ServerListen()
        {
            tcpClient = new TcpListener(IPAddress.Any, Config.getInstance().targetPort);
            tcpClient.Start();
            try
            {
                do
                {
                    ClientHandler handler = new ClientHandler(this, tcpClient.AcceptSocket());
                    Console.WriteLine("クライアントが接続してきました");
                    hClient.Add(handler);
                    handler.StartRead();
                } while (true);
            }
            catch
            {
                return;
            }
        }
        public List<ClientHandler> getClient()
        {
            return this.hClient;
        }
        private void deleteClient(ClientHandler cl)
        {
            Console.WriteLine("クライアントが抜けました");
            foreach (ClientHandler handler in hClient)
            {
                if (handler == cl)
                {
                    hClient.Remove(cl);
                    break;
                }
            }
        }
        public void CloseServer()
        {
            if (tcpClient != null)
            {
                tcpClient.Stop();
                Thread.Sleep(20);
                tcpClient = null;
            }
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        public class ClientHandler
        {
            private SocketServer parent;
            private byte[] buffer;
            private Socket socket;
            private NetworkStream networkStream;
            private AsyncCallback callbackRead;
            private AsyncCallback callbackWrite;

            public ClientHandler(SocketServer parent, Socket socketForClient)
            {
                this.socket = socketForClient;
                this.parent = parent;
                buffer = new byte[256];
                networkStream = new NetworkStream(socketForClient);
                callbackRead = new AsyncCallback(this.OnReadComplete);
                callbackWrite = new AsyncCallback(this.OnWriteComplete);
            }

            public IntPtr ClientHandle
            {
                get { return socket.Handle; }
            }
            public void StartRead()
            {
                networkStream.BeginRead(buffer, 0, buffer.Length, callbackRead, null);
            }

            private void OnReadComplete(IAsyncResult ar)
            {
                try
                {
                    if (networkStream == null)
                        return;
                    int bytesRead = networkStream.EndRead(ar);
                    if (bytesRead > 0)
                    {
                        Byte[] getByte = new byte[bytesRead];
                        for (int i = 0; i < bytesRead; i++)
                            getByte[i] = buffer[i];
                        parent.listener.onReceive(parent.ecUni.GetString(Encoding.Convert(parent.ecSjis, parent.ecUni, getByte)));
                        StartRead();

                    }
                    else
                    {
                        parent.deleteClient(this);
                        networkStream.Close();
                        socket.Close();
                        networkStream = null;
                        Thread.Sleep(20);
                        socket = null;
                        parent.listener.onClose(SocketCloseType.CORRECT);
                    }
                }
                catch
                {
                    return;
                }
            }
            public void WriteString(byte[] buffer)
            {
                networkStream.BeginWrite(buffer, 0, buffer.Length, callbackWrite, null);
            }
            private void OnWriteComplete(IAsyncResult ar)
            {
                networkStream.EndWrite(ar);
            }
        }
    }
}
