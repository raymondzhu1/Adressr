using Microsoft.AspNetCore.Mvc;

namespace AdressrNET.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
