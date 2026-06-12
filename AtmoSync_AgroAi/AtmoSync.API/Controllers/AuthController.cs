using Microsoft.AspNetCore.Mvc;

namespace AtmoSync.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
