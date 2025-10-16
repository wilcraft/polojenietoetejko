using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal class ServerLogic
    {
        private static ConcurrentDictionary<string, TcpClient> _clients = new ConcurrentDictionary<string,TcpClient>();
        private static ConcurrentDictionary<TcpClient, string> _inverseClients = new ConcurrentDictionary<TcpClient, string>();
        internal static async Task CreateServer(string IP, string Port)
        {
            IPEndPoint _EndPoint;
            int _port = 0;
            TcpListener _Listener;
            if (!int.TryParse(Port, out _port))
            {
                return;
            }
            else
            {
                _EndPoint = new IPEndPoint(IPAddress.Parse(IP), _port);
            }

            _Listener = new(_EndPoint);

            try
            {

                _Listener.Start();
                Console.WriteLine($"Server started at: {IP}:{Port}...");
                while (true)
                {
                    TcpClient client = await _Listener.AcceptTcpClientAsync();
                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string handshakeMessage = Encoding.UTF8.GetString(buffer,0,bytesRead);

                    if (handshakeMessage.StartsWith("HANDSHAKE:"))
                    {
                        string username = handshakeMessage.Substring("HANDSHAKE:".Length);
                        _clients[username] = client;

                        Console.WriteLine($"Handshake successful!: {username}");
                        _ = Task.Run(() => HandleClientAsync(username));

                    }
                }
            }
            finally
            {
                _Listener.Stop();
            }
        }
        internal static async Task HandleClientAsync(string username)
        {
                TcpClient client = _clients[username];
                using NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if(bytesRead == 0)
                    {
                        Console.WriteLine($"Client Disconnected");
                        break;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"{username}: {message}");
                }
            _clients.TryRemove(username, out _);
        }
    }
}
