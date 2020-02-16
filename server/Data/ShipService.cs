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

        public void SendDirections(ClientCommands commands)
        {
            Console.WriteLine(httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            ClientMessage message = new ClientMessage();
            message.client = GetShipId(httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            message.commands = commands.commands;

            inbox.Add(message);
        }


        public int SendDirection(string direction)
        {
            Console.WriteLine(httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            ClientMessage message = new ClientMessage();
            message.client = GetShipId(httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            message.commands = new string[1];
            message.commands[0] = direction;
            
            inbox.Add(message);
            return message.client;

        }


        // private int GetShipId(string ip)
        // {
        //     if (!clientIps.ContainsKey(ip))
        //     {
        //         clientIps[ip] = Interlocked.Increment(ref shipIndex) - 1;
        //     }

        //     return clientIps[ip];
        // }

        private Dictionary<string, int> shipDictionary = new Dictionary<string,int>()
        {
            {"192.168.0.116", 0},
            {"192.168.0.229", 4}

        };
        private int GetShipId(string ip)
        {
            return shipDictionary[ip];
        }

        public void HandleUnityConnectionAsync()
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
                }
            }



        }

        public void Start()
        {
            Task.Run(HandleUnityConnectionAsync);
        }
    }
}
