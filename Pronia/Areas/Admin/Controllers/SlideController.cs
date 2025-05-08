using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.Utilities.Enams;
using Pronia.Utilities.Extensions;
using Pronia.ViewModels;

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

            List<GetSlideVM> slideVMs = await _context.Slides.Select(s =>

                new GetSlideVM

                {

                    CreatedAt = s.CreateAt,
                    Title = s.Title,
                    Image = s.Image,
                    Id = s.Id,
                    Order = s.Order,
                }
                ).ToListAsync();

            
            return View(slideVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!slideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File type is incorrect");
                return View();
            }
            if (!slideVM.Photo.ValidateSize(FileSize.MB,1))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File size Sholud be less than 1 mb");
                return View();
            }

            string fileName = await slideVM.Photo.CreateFileAsync( _env.WebRootPath, "assets", "images", "website-images");

            Slide slide = new Slide 
            { 
              Title=slideVM.Title,
              Subtitle=slideVM.Subtitle,
              Description=slideVM.Description,
              Order=slideVM.Order,
              Image=fileName,
              CreateAt=DateTime.Now
            
            
            };


           
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {

            if (id is null || id <= 0)
            {
                return BadRequest();
            }
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null)
            {
                return NotFound();
            }
            UpdateSlideVM slideVM = new UpdateSlideVM
            {
                Title = slide.Title,
                Subtitle = slide.Subtitle,
                Description = slide.Description,
                Order = slide.Order,
                Image = slide.Image,
            };
            return View(slideVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVM slideVM)
        {
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (!ModelState.IsValid)
            {
                slideVM.Image = slide.Image;
                return View(slideVM);
            }
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();
            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File type is incorrect");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File size must be less than 1 mb ");
                    return View(slideVM);
                }
                string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image = fileName;
            }
                existed.Title = slideVM.Title;
                existed.Subtitle = slideVM.Subtitle;
                existed.Description = slideVM.Description;
                existed.Order = slideVM.Order; 
                await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
           
            //return Content ("ok");
        } 
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id<=0)
            {
                return BadRequest();
            }
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null)
            {
                return NotFound();
            }
          

            slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
