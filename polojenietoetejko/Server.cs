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
            string defaultIP = "0.0.0.0:25565";
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = defaultIP;
            }
            address = IPAddress.Parse(ipAddress.Split(':').First());
            if(!int.TryParse(ipAddress.Split(":").Last(), out port)){
                throw new Exception("greshka");
            }
            remoteEndPoint = new IPEndPoint(address, port);
            this.form = form;

            Timer clientTimeoutMonitor = new Timer(10000);
            clientTimeoutMonitor.Elapsed += HeartbeatCheck;
            clientTimeoutMonitor.Start();

        }
        public async Task CreateServer()
        { 
            TcpListener listener = new TcpListener(this.remoteEndPoint);
            try
            {
                listener.Start();
                ServerDiscovery.StartDiscoveryServer(remoteEndPoint.Address,remoteEndPoint.Port);
                Console.WriteLine($"Server Started at: {this.address}:{this.port}");
                Console.WriteLine($"Discovery Server listening on: {ServerDiscovery.DiscoveryPort} UDP!");

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
                        ClientManager.Instance.AddClient(username, tempTcpClient, GetClientIP(tempTcpClient));
                        Console.WriteLine($"Handshake Successful!: {username}");
                        Console.WriteLine(ClientManager.Instance.GetHashCode());
                        var client = ClientManager.Instance.GetClient(username);
                        byte[] returnHandshake = Encoding.UTF8.GetBytes("HANDSHAKE_OK");
                        await client.UserClient.GetStream().WriteAsync(returnHandshake, 0, returnHandshake.Length);
                        _ = Task.Run(() => HandleClientAsync(client));
                    }
                }
            }
            finally
            {
                listener.Stop();
            }
        }
        private IPAddress GetClientIP(TcpClient client)
        {
            return ((IPEndPoint)client.Client.RemoteEndPoint).Address;
        }
        private async Task HandleClientAsync(Client client)
        {
            byte[] buffer = new byte[1024]; 
            Console.WriteLine($"Client Connected: {client.Username}");
            try
            {
                while (client.UserClient.Connected)
                {
                    int bytesRead = await client.UserClient.GetStream().ReadAsync(buffer,0,buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer,0, bytesRead);
                    if (message == null || message.Equals("DISCONNECT"))
                    {
                        Console.WriteLine($"{client.Username} Disconnected");
                        Console.WriteLine($"Sending DISCONNECT to {client.iPAddress}");
                        SendMessageToClient(client, message);
                        ClientManager.Instance.RemoveClient(client.Username);
                        break;
                    }
                    else if (message.Equals("PING"))
                    {
                        lastHeartbeat[client] = DateTime.Now;
                        Console.WriteLine($"Received: {client.Username}");
                    }
                    else
                    {
                        //form.UpdateChatBox($"{DateTime.Now:HH:mm} {client.Username}: {message}");
                        //SendMessageToClient(client,$"{client.iPAddress} {message} SENT");
                        BroadcastMessage(client,message);
                    }
                }
            }
            catch(Exception e)
            {
                ClientManager.Instance.RemoveClient(client.Username);
                throw;
            }
        }
        private void SendMessageToClient(Client client, string message)
        {
            try
            {
                NetworkStream stream = client.UserClient.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.WriteAsync(data, 0, data.Length);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void BroadcastMessage(Client sender, string message)
        {
            foreach(var client in ClientManager.Instance.GetClients())
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes($"{sender.Username}: {message}");
                    client.UserClient.GetStream().WriteAsync(data, 0, data.Length);
                    Console.WriteLine($"{sender.Username}: {message}");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{client.Username} failed to receive message! {e.Message}");
                }
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
