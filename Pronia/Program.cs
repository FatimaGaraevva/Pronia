using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Pronia.DAL;
using Pronia.Models;


namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }
                );
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllerRoute(
               "admin",
               "{area:exists}/{controller=home}/{action=index}/{id?}"
               );

            app.MapControllerRoute(
                "default",
                "{controller=home}/{action=index}/{id?}"
                );
          

            app.Run();
        }
    }
}
