﻿namespace Pronia.ViewModels
{
    public class UpdateSlideVM
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public IFormFile? Photo { get; set; }


    }
}
