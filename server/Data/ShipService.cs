using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;

namespace server.Data
{
    public class ShipService
    {
        private BlockingCollection<ClientMessage> inbox;
        IHttpContextAccessor httpContextAccessor;
        ConcurrentDictionary<string, int> clientIps = new ConcurrentDictionary<string, int>();
        int shipIndex = 0;

        public ShipService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            inbox = new BlockingCollection<ClientMessage>(new ConcurrentQueue<ClientMessage>());
        }

        public void SendDirection(string ip, string direction)
        {
            Console.WriteLine(httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            ClientMessage message = new ClientMessage();
            message.client = GetShipId(httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            message.commands = new string[1];
            message.commands[0] = direction;
            
            inbox.Add(message);

        }

        private int GetShipId(string ip)
        {
            if (!clientIps.ContainsKey(ip))
            {
                clientIps[ip] = Interlocked.Increment(ref shipIndex) - 1;
            }

            return clientIps[ip];
        }

        public async Task HandleUnityConnectionAsync()
        {
            while(true)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    client.Connect("127.0.0.1", 8052);
                    NetworkStream stream = client.GetStream();
                    while(true)
                    {
                        ClientMessage message = inbox.Take();
                        string jsonString = JsonSerializer.Serialize(message);
                        string jsonSocketString = jsonString.Length + "#" + jsonString;
                        Console.WriteLine(jsonSocketString);
                        byte[] buf = Encoding.ASCII.GetBytes(jsonSocketString);
                        stream.Write(buf, 0, buf.Length);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    await Task.Delay(1000);
                }
            }



        }

        public void Start()
        {
            Task.Run(HandleUnityConnectionAsync);
        }
    }
}
