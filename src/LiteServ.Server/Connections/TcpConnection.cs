using System;
using System.IO;
using System.Net.Sockets;

namespace LiteServ.Server.Connections
{
    public class TcpConnection : IConnection
    {
        
        public void Start(int port, Action<Byte[]> messageReceived)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var stream = new NetworkStream(socket);
            var streamReader = new StreamReader(stream);
            
        }

        public void Stop()
        {
            
        }
    }
}