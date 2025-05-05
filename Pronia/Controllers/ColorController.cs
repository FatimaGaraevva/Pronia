using Microsoft.AspNetCore.Mvc;
using Pronia.DAL;

namespace Pronia.Controllers
{
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Color()
        {
            return View();
        }
    }
}
