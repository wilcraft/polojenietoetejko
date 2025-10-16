using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal class ClientLogic
    {
        internal static TcpClient client;
        internal static NetworkStream stream;
        internal static async Task ConnectToServeraAsync(string IP, string Port, string Username)
        {
            IPEndPoint _EndPoint;
            int _port = 0;
            string handshake = string.Empty;
            if (!int.TryParse(Port, out _port))
            {
                return;
            }
            else
            {
                _EndPoint = new IPEndPoint(IPAddress.Parse(IP), _port);
            }
            try
            {
                client = new();
                await client.ConnectAsync(_EndPoint);
                if (client.Connected)
                {
                    stream = client.GetStream();
                    Console.WriteLine("Connection successful!");
                    Console.WriteLine($"Client Username: {Username}");
                    handshake = $"HANDSHAKE:{Username}";
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(handshake));
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message);
            }
        }
        internal static async Task SendMessageAsync(string message)
        {
            if (client != null && client.Connected)
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
            }
        }

    }
}
