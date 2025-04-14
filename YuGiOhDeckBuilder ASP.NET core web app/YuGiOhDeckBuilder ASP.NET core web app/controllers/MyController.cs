using Microsoft.AspNetCore.Mvc;

namespace YuGiOhDeckBuilder_ASP.NET_core_web_app.controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
