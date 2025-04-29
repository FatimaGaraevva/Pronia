using Microsoft.AspNetCore.Mvc;
using Pronia.DAL;
using Pronia.Models;
using Pronia.ViewModels;
using System;
using System.Collections.Generic;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Slide> slides = new List<Slide>
            {
                new Slide
                {
                    Title="Basliq 1",
                    Subtitle="Komekci basliq",
                    Description="Nergiz gulu",
                    Image="1-1-524x617.png",
                    Order=1,
                    CreateAt=DateTime.Now
                },
                new Slide
                {
                    Title="Basliq 2",
                    Subtitle="Komekci basliq2",
                    Description="Maviqizilgul",
                    Image="1-2-524x617.png",
                    Order=2,
                    CreateAt=DateTime.Now

                },
                new Slide
                {
                    Title="Basliq 3",
                    Subtitle="Komekci basliq3",
                    Description="Yasemen",
                    Image="1-2-524x617.png",
                    Order=3,
                    CreateAt=DateTime.Now
                }


            };
            _context.Slides.AddRange(slides);
            _context.SaveChanges();

            HomeVM homeVM = new HomeVM
            {
                Slides = slides.OrderBy(s => s.Order).Take(2).ToList()
            };


            return View(homeVM);
        }
    }
}
