using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError(nameof(Slide.Photo), "File type is incorrect");
                return View();
            }
            if (slide.Photo.Length > 2*1024*1024)
            {
                ModelState.AddModelError(nameof(Slide.Photo), "File size Sholud be less than 2 mb");
                return View();
            }
            string fileName = string.Concat(Guid.NewGuid().ToString(), slide.Photo.FileName);
            string path = Path.Combine(_env.WebRootPath, "assets","images","website-images" , fileName);
            FileStream fl = new FileStream(path, FileMode.Create);
            await slide.Photo.CopyToAsync(fl);
            slide.Image = slide.Photo.FileName;
            //return Content(slide.Photo.FileName + "" + slide.Photo.ContentType + "" + slide.Photo.Length);


            slide.CreateAt = DateTime.Now;
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
