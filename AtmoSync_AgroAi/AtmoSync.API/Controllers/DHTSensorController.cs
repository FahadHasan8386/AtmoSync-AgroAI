using Microsoft.AspNetCore.Mvc;

namespace AtmoSync.API.Controllers
{
    public class DHTSensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
