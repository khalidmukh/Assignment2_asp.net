using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Models;
using AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM.Controllers
{
    public class MainController : Controller
    {
        public IActionResult login()
        {
            ViewData["error"] = "";
            return View();
        }
        [HttpPost]
        public IActionResult login(Login model)
        {

            String connstring = "server=DESKTOP-K0HLOGO\\SQLEXPRESS;database=asp;integrated security=true; TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();
                String query = $"select count(*) total from Users  where username='{model.Username}'and password='{model.Password}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0) 
                {
                    //user is valid
                    //create session
                    HttpContext.Session.SetString("username", model.Username);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Username),
                        new Claim(ClaimTypes.Role,"Admin")

                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var princible = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(princible);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //user is invalid
                    ViewData["error"] = "Invalid Credentials";
                    return View(model);
                }
            }

        }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }

        

        CategoryRepository repo;
        public MainController()
        {
            repo = new CategoryRepository();
        }
        [Authorize]
        public IActionResult Index()
        {
            
            try
            {
                var data = repo.getAll();
                return View(data);

            }
            catch (Exception ex)
            {
                ViewData["error"] = "An error occurred while processing your request.";
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize]
        public IActionResult Create()
        {
            return View(); 
        }
        
        [HttpPost]
        public IActionResult Create(Product model)
        {
            repo.create(model.Id,model.Name,model.Price,model.Description);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var found = repo.get_by_id(id);
            return View(found);
        }
        
        [HttpPost]
        public IActionResult Edit(Product model)
        {
            repo.update(model.Id, model.Name, model.Price, model.Description);
            return RedirectToAction("Index");
        }
        [Authorize]

        public IActionResult Delete(int id)
        {
            var found = repo.get_by_id(id);
            return View(found);
        }
       
        [HttpPost]
        public IActionResult Delete(Product model)
        {

            repo.delete(model.Id);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult About()
        {
            return View();
        }

    }
}
