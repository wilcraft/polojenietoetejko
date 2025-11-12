using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal static class ServerDiscovery
    {
        internal const int DiscoveryPort = 9999;
        private const string discoveryMessage = "DISCOVERME";
        private const string discoveryResponse = "SERVER_FOUND/";
        public static void StartDiscoveryServer(IPAddress ip, int port)
        {
            
            UdpClient server = new(DiscoveryPort);
            IPEndPoint remoteEndPoint = new(IPAddress.Parse("0.0.0.0"), 0);
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    UdpReceiveResult result = await server.ReceiveAsync();
                    string msg = Encoding.UTF8.GetString(result.Buffer);
                    if (msg.Equals(discoveryMessage))
                    {
                        byte[] data = Encoding.UTF8.GetBytes($"{discoveryResponse}{getLocalIP()}:{port}");
                        await server.SendAsync(data, data.Length, result.RemoteEndPoint);
                    }
                }
            });
        }
        public static async Task<List<string>> ClientDisocverServersAsync()
        {
            List<string> serverList = new();
            using(UdpClient client = new())
            {
                client.EnableBroadcast = true;
                IPEndPoint broadcastingEndpoint = new(IPAddress.Broadcast, DiscoveryPort);

                byte[] request = Encoding.UTF8.GetBytes(discoveryMessage);
                await client.SendAsync(request, request.Length, broadcastingEndpoint);

                int timeoutTimer = 3;
                while(DateTime.Now < DateTime.Now.AddSeconds(timeoutTimer))
                {
                    if(client.Available > 0)
                    {
                        UdpReceiveResult result = await client.ReceiveAsync();
                        string response = Encoding.UTF8.GetString(result.Buffer);

                        if (response.StartsWith(discoveryResponse))
                        {
                            serverList.Add(response.Split("/").Last());
                        }
                        return serverList;
                    }
                    await Task.Delay(250);
                }
            }
            return serverList;
        }
        private static string getLocalIP()
        {
            foreach(var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
    }
}
