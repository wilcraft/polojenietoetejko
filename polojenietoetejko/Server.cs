using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace polojenietoetejko
{
    internal class Server
    {
        private Form1 form;
        private IPAddress address;
        private int port;
        private IPEndPoint remoteEndPoint;

        ConcurrentDictionary<Client, DateTime> lastHeartbeat = new();
        public Server(string ipAddress, Form1 form)
        {
            address = IPAddress.Parse(ipAddress.Split(':').First());
            if(!int.TryParse(ipAddress.Split(":").Last(), out port)){
                throw new Exception("greshka");
            }
            remoteEndPoint = new IPEndPoint(address, port);
            this.form = form;

            Timer clientTimeoutMonitor = new Timer(10000);
            clientTimeoutMonitor.Elapsed += HeartbeatCheck;
            clientTimeoutMonitor.AutoReset = true;
            clientTimeoutMonitor.Start();

        }
        public async Task CreateServer()
        { 
            TcpListener listener = new TcpListener(this.remoteEndPoint);
            try
            {
                listener.Start();
                Console.WriteLine($"Server Started at: {this.address}:{this.port}");

                while (true)
                {
                    TcpClient tempTcpClient = await listener.AcceptTcpClientAsync();
                    NetworkStream stream = tempTcpClient.GetStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string handshakeMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (handshakeMessage.StartsWith("HANDSHAKE:"))
                    {
                        string username = handshakeMessage.Substring("HANDSHAKE:".Length).Trim().ToLower();
                        ClientManager.Instance.AddClient(username, tempTcpClient);
                        Console.WriteLine($"Handshake Successful!: {username}");
                        Console.WriteLine(ClientManager.Instance.GetHashCode());
                        var client = ClientManager.Instance.GetClient(username);
                        _ = Task.Run(() => HandleClientAsync(client));
                    }
                }
            }
            finally
            {
                listener.Stop();
            }
        }
        private async Task HandleClientAsync(Client client)
        {
            Console.WriteLine($"Client Connected: {client.Username}");
            try
            {
                while (true)
                {
                    string message = await client.ReadMessageAsync();
                    if (message == null)
                    {
                        ClientManager.Instance.RemoveClient(client.Username);
                        break;
                    }
                    if (message.Equals("PING"))
                    {
                        lastHeartbeat[client] = DateTime.Now;
                        Console.WriteLine($"Received: {client.Username}");
                    }
                    else
                    {
                        form.UpdateChatBox($"{DateTime.Now:HH:mm} {client.Username}: {message}");
                        Console.WriteLine($"{client.Username}: {message}");
                    }
                }
            }
            catch(Exception)
            {
                ClientManager.Instance.RemoveClient(client.Username);
                throw;
            }
        }
        
        private void HeartbeatCheck(object sender, ElapsedEventArgs e)
        {
            foreach(var kvp in lastHeartbeat)
            {
                if((DateTime.Now - kvp.Value).TotalSeconds > 15)
                {
                    ClientManager.Instance.RemoveClient(kvp.Key.Username);
                }
            }
        }
    }
}
