using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; } = 0;

        public ICollection<Category> Categories { get; set; }
    }
}
