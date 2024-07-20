using System;
using System.Net;
using System.Net.Sockets;

namespace Shared.Network
{
    public class Connection
    {
        public IPAddress Local => ((IPEndPoint)socket.LocalEndPoint).Address;
        public IPAddress Remote => ((IPEndPoint)socket.RemoteEndPoint).Address;
        public byte[] Buffer { get; } = new byte[30000];
        public int ReceiveLength { get; private set; }

        public int ReceiveTimeout
        {
            get => socket.ReceiveTimeout;
            set => socket.ReceiveTimeout = value;
        }
        
        private readonly Socket socket;
        private Action receiveCallback;

        public Connection(Socket socket)
        {
            this.socket = socket;
            socket.NoDelay = true;
        }

        public void Close()
        {
            socket.Close();
        }

        public void Receive()
        {
            try
            {
                ReceiveLength = socket.Receive(Buffer, 0, Buffer.Length, SocketFlags.None);
            }
            catch (SocketException exception)
            {
                #if DEBUG
                    Console.WriteLine(exception.Message);
                #endif
                throw;
            }
        }

        public void BeginReceive(Action callback)
        {
            try
            {
                receiveCallback = callback;
                socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, BeginReceiveCallback, null);
            }
            catch
            {
            
            }
        }

        private void BeginReceiveCallback(IAsyncResult result)
        {
            try
            {
                ReceiveLength = socket.EndReceive(result);
                receiveCallback();
            }
            catch
            {
            }
        }

        public void Send(byte[] buffer)
        {
            socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
