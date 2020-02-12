using Microsoft.AspNetCore.Mvc;
using server.Data;
namespace server
{
    [Route("[controller]/json")]
    [ApiController]
    public class ShipController : Controller
    {
        ShipService shipService;

        public ShipController(ShipService shipService)
        {
            this.shipService = shipService;
        }

        [HttpPost]
        public ActionResult<string> Index(ClientCommands message)
        {
            shipService.SendDirections(message);
            return "Success";
        }
    }

        public class ClientCommands 
    {
        public string[] commands { get; set; }
    }
}
