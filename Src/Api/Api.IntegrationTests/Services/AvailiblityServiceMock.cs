using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Api.IntegrationTests.Services
{
    public class AvailiblityServiceMock : IDisposable
    {
        // The products to return information for
        public static Dictionary<string, int> ProductCount = new Dictionary<string, int>();

        // Interal data
        private readonly Thread _connectionThread;
        private readonly IPEndPoint _endpoint;
        private Socket _socket;

        public AvailiblityServiceMock()
        {
            _connectionThread = new Thread(Run);
            // We only need to listen to local host
            var listenIp = IPAddress.Loopback;
            _endpoint = new IPEndPoint(listenIp, 5001);
            _socket = new Socket(listenIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.ReceiveTimeout = 1000;

            _connectionThread.Start();
        }

        public void Dispose()
        {
            _socket?.Dispose();
            _socket = null;
            _connectionThread.Abort();
        }

        private void Run()
        {
            // Bind on the socket
            _socket.Bind(_endpoint);
            // Start listening for incomming connections
            _socket.Listen(100);
            try
            {
                while (_socket != null)
                {
                    var connection = _socket.Accept();
                    while (true)
                    {
                        // read the header
                        var sizeBuffer = new byte[2];
                        connection.Receive(sizeBuffer);
                        // read the content
                        var size = BitConverter.ToInt16(sizeBuffer, 0);
                        var productIdBuffer = new byte[size];
                        connection.Receive(productIdBuffer);
                        // Since this is a stub directly parse the buffer as an ASCII string
                        var id = Encoding.ASCII.GetString(productIdBuffer);
                        var count = 0;
                        // Check if we have the product
                        if (ProductCount.ContainsKey(id))
                            count = ProductCount[id];
                        var responseBuffer = BitConverter.GetBytes(count);

                        connection.Send(responseBuffer);
                    }
                }
            }
            catch (Exception)
            {
                // Ignore
            }
        }
    }
}