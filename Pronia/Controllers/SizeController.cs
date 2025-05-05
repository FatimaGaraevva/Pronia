using Microsoft.AspNetCore.Mvc;
using Pronia.DAL;

namespace Pronia.Controllers
{

    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Size()
        {
            return View();
        }
    }
}
