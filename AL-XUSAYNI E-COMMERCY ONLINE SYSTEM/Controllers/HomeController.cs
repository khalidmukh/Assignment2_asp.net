using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis;


namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        orderDetails Detail;
        public HomeController()
        {
            Detail = new orderDetails();    
        }
        public IActionResult Index(resource model)
        {
            try
            {
                var found_orders = Detail.list();
                return View(found_orders);
            }
            catch (Exception ex)
            {
                ViewData["error"] = "An error occurred while processing your request.";
                return RedirectToAction("Error", "Home");
            }
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
    }
}
