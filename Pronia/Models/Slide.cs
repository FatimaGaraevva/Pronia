

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Models
{
    public class Slide:BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
