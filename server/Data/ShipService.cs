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
            Task.Run(HandleUnityConnection);
        }

        public bool SendDirectionAsync(string direction)
        {
            return true;
        }

        public void HandleUnityConnection()
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 13000);

        }
    }
}
