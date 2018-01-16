using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Api.Configuration;
using Serilog;

namespace Api.Repositories
{
    /// <inheritdoc />
    public class AvalibilityRepository : IAvailabilityRepository
    {
        private readonly IPEndPoint _endpoint;
        private readonly Socket _socket;

        /// <summary>
        /// Initialize this instance and setup and connect the socket
        /// </summary>
        /// <param name="configuration"></param>
        public AvalibilityRepository(IConfiguration configuration)
        {
            _endpoint = configuration.GetAvailibilityServiceEndPoint();
            _socket = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Connect()
        {
            if (_socket.Connected)
                return;
            _socket.Connect(_endpoint);
        }

        /// <inheritdoc />
        public int? GetProductCount(string productId)
        {
            try
            {
                Connect();
            }
            catch (SocketException ex)
            {
                Log.Warning(ex, "Unable to contact availibility service");
                return null;
            }

            var stringBuffer = Encoding.ASCII.GetBytes(productId);
            var buffer = new byte[stringBuffer.Length + 2];
            // Create the data to send
            // (StrLength)(ProductId)
            // StrLength -> INT16
            // ProductId -> STRING
            Buffer.BlockCopy(BitConverter.GetBytes((short)stringBuffer.Length), 0, buffer, 0, 2);
            Buffer.BlockCopy(stringBuffer, 0, buffer, 2, stringBuffer.Length);
            _socket.Send(buffer);

            // Result
            // (ProductCount)
            // ProductCount -> INT32
            buffer = new byte[4];
            _socket.Receive(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}