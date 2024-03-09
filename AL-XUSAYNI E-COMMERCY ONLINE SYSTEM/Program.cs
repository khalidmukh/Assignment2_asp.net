using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AL_XUSAYNI_E_COMMERCY_ONLINE_SYSTEM
{

    [Authorize]
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            ///SESSTION
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.Name = "asp";
                options.Cookie.Path = "/";
                options.Cookie.HttpOnly = true; //more secure, only the server can read the cookie
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.IsEssential = true;

            });
            ///AUTHENTICATION

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Main/login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}