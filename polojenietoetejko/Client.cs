using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal class Client
    {

        //SHTE POLUDEQ
        private static Client? instance;
        private string username;
        private TcpClient client;
        private IPAddress address;
        public Client() { }
        internal Client(string username, TcpClient client)
        {
            this.username = username;
            this.client = client;
            this.address = GetAddress();
        }
        public static void Initialize(string username, TcpClient client)
        {
            if (instance == null)
            {
                instance = new Client(username,client);
            }
        }
        public static void Reset()
        {
            instance = null;
        }
        private IPAddress GetAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 in the network");
        }
        public static async Task ConnectToServerAsync(string ipAddress, string username)
        {  
            IPAddress address = IPAddress.Parse(ipAddress.Split(':').First());
            int port = 0;
            if (!int.TryParse(ipAddress.Split(":").Last(), out port))
            {
                throw new Exception("greshka");
            }
            string handshake = string.Empty;
            IPEndPoint remoteEndPoint = new IPEndPoint(address,port);
            try
            {
                TcpClient client = new();
                await client.ConnectAsync(remoteEndPoint);
                if (client.Connected)
                {
                    NetworkStream stream = client.GetStream();
                    handshake = $"HANDSHAKE:{username}";
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(handshake));
                    Initialize(username, client);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<String> ReadMessageAsync()
        {
            NetworkStream stream = this.client.GetStream();
            var buffer = new byte[1024];

            try
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if(bytesRead == 0)
                {
                    return null;
                }

                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (IOException e)
            {
                return e.ToString();
            }
        }
        public async Task SendMessageAsync(string message)
        {
            if(this.client != null && this.client.Connected)
            {
                NetworkStream stream = this.client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
            }
        }
        public string Username { get { return username; } }
        public static Client Instance { get { return instance; } }
    }
}
