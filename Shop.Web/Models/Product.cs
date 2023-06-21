using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int DiscountId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
