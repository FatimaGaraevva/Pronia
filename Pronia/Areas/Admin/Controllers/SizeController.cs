using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await _context.Sizes.ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Size size )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Sizes.AnyAsync(c => c.Name == size.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(size.Name), $"{size.Name} name already exists");
                return View();
            }
            size.CreateAt = DateTime.Now;
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest();
            }

            Size? size = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == id);
            if (size is null)
            {
                return NotFound();
            }


            return View(size);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Sizes.AnyAsync(c => size.Name == c.Name && c.Id != id);
            if (result)
            {
                ModelState.AddModelError(nameof(size.Name), $"{size.Name} name already exists");
                return View();
            }

            Category? existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            existed.Name = size.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}


    

