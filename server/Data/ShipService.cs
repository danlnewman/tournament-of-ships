using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace server.Data
{
    public class ShipService
    {
        public ShipService()
        {
        }

        public bool SendDirectionAsync(string direction)
        {
            return true;
        }

        public async Task HandleUnityConnectionAsync()
        {
            TcpClient client = new TcpClient();
            while(true)
            {
                try
                {
                    client.Connect("127.0.0.1", 13000);
                    break;
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
