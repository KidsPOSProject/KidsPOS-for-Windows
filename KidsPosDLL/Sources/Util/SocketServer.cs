using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KidsPos.Object;

namespace KidsPos.Util
{
    public class SocketServer : SocketBase
    {
        private static readonly SocketServer Instance = new SocketServer();
        private readonly List<ClientHandler> _hClient = new List<ClientHandler>();

        private TcpListener _tcpClient;

        private SocketServer()
        {
        }

        public static SocketServer GetInstance()
        {
            return Instance;
        }

        public bool ServerStart()
        {
            try
            {
                Thread = new Thread(ServerListen);
                Thread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ServerListen()
        {
            _tcpClient = new TcpListener(IPAddress.Any, Config.GetInstance().TargetPort);
            _tcpClient.Start();
            try
            {
                do
                {
                    var handler = new ClientHandler(this, _tcpClient.AcceptSocket());
                    Console.WriteLine("クライアントが接続してきました");
                    _hClient.Add(handler);
                    handler.StartRead();
                } while (true);
            }
            catch
            {
                // ignored
            }
        }

        public List<ClientHandler> GetClient()
        {
            return _hClient;
        }

        private void DeleteClient(ClientHandler cl)
        {
            Console.WriteLine("クライアントが抜けました");
            if (_hClient.Any(handler => handler == cl)) _hClient.Remove(cl);
        }

        public void CloseServer()
        {
            if (_tcpClient != null)
            {
                _tcpClient.Stop();
                Thread.Sleep(20);
                _tcpClient = null;
            }

            if (Thread == null) return;
            Thread.Abort();
            Thread = null;
        }

        public class ClientHandler
        {
            private readonly byte[] _buffer;
            private readonly AsyncCallback _callbackRead;
            private readonly AsyncCallback _callbackWrite;
            private readonly SocketServer _parent;
            private NetworkStream _networkStream;
            private Socket _socket;

            public ClientHandler(SocketServer parent, Socket socketForClient)
            {
                _socket = socketForClient;
                _parent = parent;
                _buffer = new byte[256];
                _networkStream = new NetworkStream(socketForClient);
                _callbackRead = OnReadComplete;
                _callbackWrite = OnWriteComplete;
            }

            public IntPtr ClientHandle => _socket.Handle;

            public void StartRead()
            {
                _networkStream.BeginRead(_buffer, 0, _buffer.Length, _callbackRead, null);
            }

            private void OnReadComplete(IAsyncResult ar)
            {
                try
                {
                    if (_networkStream == null)
                        return;
                    var bytesRead = _networkStream.EndRead(ar);
                    if (bytesRead > 0)
                    {
                        var getByte = new byte[bytesRead];
                        for (var i = 0; i < bytesRead; i++)
                            getByte[i] = _buffer[i];
                        _parent.Listener.OnReceive(
                            _parent.EcUni.GetString(Encoding.Convert(_parent.EcSjis, _parent.EcUni, getByte)));
                        StartRead();
                    }
                    else
                    {
                        _parent.DeleteClient(this);
                        _networkStream.Close();
                        _socket.Close();
                        _networkStream = null;
                        Thread.Sleep(20);
                        _socket = null;
                        _parent.Listener.OnClose(SocketCloseType.Correct);
                    }
                }
                catch
                {
                    // ignored
                }
            }

            public void WriteString(byte[] buffer)
            {
                _networkStream.BeginWrite(buffer, 0, buffer.Length, _callbackWrite, null);
            }

            private void OnWriteComplete(IAsyncResult ar)
            {
                _networkStream.EndWrite(ar);
            }
        }
    }
}