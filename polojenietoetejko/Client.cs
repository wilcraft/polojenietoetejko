using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;


namespace polojenietoetejko
{
    internal class Client
    {

        //SHTE POLUDEQ
        private static Client? instance;
        private string username;
        private TcpClient client;
        private IPAddress address;
        private Timer heartbeatClientsideTimer;
        internal Client(string username, TcpClient client, IPAddress address)
        {
            this.username = username;
            this.client = client;
            this.address = address;

            heartbeatClientsideTimer = new(5000);
            heartbeatClientsideTimer.Elapsed += PingServer;
            heartbeatClientsideTimer.Start();
        }
        public static void Initialize(string username, TcpClient client, IPAddress address)
        {
            if (instance == null)
            {
                instance = new Client(username,client,address);
            }
        }
        public void Reset()
        {
            instance = null;
        }
        private static IPAddress GetAddress()
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
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(handshake),0,handshake.Length);
                    byte[] tempBuffer = new byte[1024];
                    int tempBytesRead = await stream.ReadAsync(tempBuffer,0,tempBuffer.Length);
                    string serverResponse = Encoding.UTF8.GetString(tempBuffer,0,tempBytesRead).Trim();
                    if (serverResponse.Equals("HANDSHAKE_OK"))
                    {
                        Initialize(username, client, GetAddress());
                        
                        _ = Task.Run(() => Instance.ReadMessageAsync());
                    }
                    else
                    {
                        Console.WriteLine("Handshake Failed!");
                        client.Close();
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DisconnectClient()
        {
            this.client.Close();
            this.Reset();
        }
        public async Task ReadMessageAsync()
        {
            NetworkStream stream = this.client.GetStream();
            var buffer = new byte[1024];
            while (this.client.Connected)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (message.Equals("DISCONNECT"))
                    {
                        this.DisconnectClient();
                    }
                    Console.WriteLine(message);
                    
                }
                catch (IOException e)
                {
                    
                }
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
        public void PingServer(object sender, ElapsedEventArgs e)
        {
            try
            {
                NetworkStream stream = this.client.GetStream();
                byte[] pingMessage = Encoding.UTF8.GetBytes("PING");
                stream.WriteAsync(pingMessage, 0, pingMessage.Length);
            }
            catch
            {
                heartbeatClientsideTimer.Stop();
            }
        }
        public string Username { get { return username; } }
        public TcpClient UserClient { get { return client; } }
        public IPAddress iPAddress { get { return address; } }
        public static Client Instance { get { return instance; } }
    }
}
