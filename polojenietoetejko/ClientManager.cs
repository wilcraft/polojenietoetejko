using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace polojenietoetejko
{
    internal class ClientManager
    {
        //pisna mi chestno
        private static readonly ClientManager instance = new();

        private readonly ConcurrentDictionary<string, Client> clients = new();
        public bool AddClient(string username, TcpClient client)
        {
            if (clients.ContainsKey(username))
            {
                return false;
            }           
            Console.WriteLine($"Client Added {username}");
            
            return clients.TryAdd(username, new Client(username, client));
        }

        public bool RemoveClient(string username)
        {
            Client client = GetClient(username);
            if(client == null)
            {
                return false;
            }
            if (client.UserClient.Connected)
            {
                client.DisconnectClient();
                return clients.TryRemove(username, out _);
            }
            else
            {
                return clients.TryRemove(username, out _);
            }
        }

        public Client GetClient(string username)
        {
            clients.TryGetValue(username, out Client client);
            return client;
        }
        public IEnumerable<Client> GetClients()
        {
            return clients.Values;
        }
        public static ClientManager Instance { get { return instance; } }
    }
}
