using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Models;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        CategoryRepository Product;
        orderDetails Detail;

        public ProductController()
        {
            Product = new CategoryRepository();
            Detail = new orderDetails();
        }
        public IActionResult Index()
        {
            try
            {
                var products = Product.GetProducts();
                return View(products);
            }
            catch (Exception ex)
            {
                ViewData["error"] = "An error occurred while processing your request.";
                return RedirectToAction("Error", "Home");
            }

            
        }

        public IActionResult Order(int Id)
        {
            try
            {
                var found = Product.get_order(Id);
                return View(found);
            }
            catch (Exception ex)
            {
                ViewData["error"] = "An error occurred while processing your request.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Order(resource model)
        {
            try
            {
                Detail.Order(model.Name, model.Id, model.Quentity, model.Price, model.Description);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewData["error"] = "An error occurred while processing your request.";
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult order_list(resource model)
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
    }
}