using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Backend.IntegrationTests.Helpers
{
    public static class AvailibilityServerHelper
    {
        public static int GetProductCount(string productId)
        {
            var ip = IPAddress.Parse("127.0.0.1");
            var port = 5000;
            var ipEndpoint = new IPEndPoint(ip, port);
            using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                for (var i = 0; i < 500; i++)
                {
                    try
                    {
                        socket.Connect(ipEndpoint);
                        break;
                    }
                    catch (SocketException _)
                    {
                        if (i == 499)
                            throw;
                        Thread.Sleep(100);
                    }
                }

                var stringBuffer = Encoding.ASCII.GetBytes(productId);
                var buffer = new byte[stringBuffer.Length + 2];
                // Create the data to send
                // (StrLength)(ProductId)
                // StrLength -> INT16
                // ProductId -> STRING
                Buffer.BlockCopy(BitConverter.GetBytes((short)stringBuffer.Length), 0, buffer, 0, 2);
                Buffer.BlockCopy(stringBuffer, 0, buffer, 2, stringBuffer.Length);
                socket.Send(buffer);

                // Result
                // (ProductCount)
                // ProductCount -> INT32
                buffer = new byte[4];
                socket.Receive(buffer);
                return BitConverter.ToInt32(buffer, 0);
            }
        }
    }
}
