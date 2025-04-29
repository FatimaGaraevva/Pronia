using Microsoft.EntityFrameworkCore;
using Pronia.Controllers;
using Pronia.DAL;
using System;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer("server=WINDOWS-8K5SI44\\SQLEXPRESS;database=ProniaDB;trusted_connection=true;integrated securitiy=true;TrustServerCertifcate=true;");
            }
                );
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                "default",
                "{controller=home}/{action=index}/{id?}"



                );
          

            app.Run();
        }
    }
}
