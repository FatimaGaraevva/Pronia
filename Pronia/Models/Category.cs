using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    
    public class Category:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        //relational
        public List<Product>? Products { get; set; }
    }
}
