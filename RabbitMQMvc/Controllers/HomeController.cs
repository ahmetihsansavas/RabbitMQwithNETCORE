using Microsoft.AspNetCore.Mvc;
using RabbitMQMvc.Models;
using System.Diagnostics;

namespace RabbitMQMvc.Controllers
{
    public class HomeController : Controller
    {
        static private Dictionary<string, string> ChatList = new Dictionary<string, string>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Login() {          
            return View(new LoginModel());
        }
        [HttpPost]
        public IActionResult Login(string name) {

            if (!ChatList.Any(cl => cl.Value.ToUpper() == name.ToUpper()) && name.Trim() != "")
            {
                ChatList.Add(DateTime.Now.ToString(), name);
                HttpContext.Session.SetString("UserName", name);
                return RedirectToAction("CreateMessage","Message");
            }
            else
            {
                return Error();
            }
        }
    }
}