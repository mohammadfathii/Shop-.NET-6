using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Models.ViewModel
{
    public class CreateUpdateProductViewModel
    {
        [AllowNull]
        public int Id { get; set; } = 0;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; } = 10000;
        [Required]
        public int QuantityInStock { get; set; }
        [AllowNull]
        [Range(0, 99)]
        public int DiscountPercent { get; set; } = 0;
        [AllowNull]
        [Range(0,9999999)]
        public int DiscountCount { get; set; } = 0;
        [Required]
        public IFormFile Thumbnail { get; set; }

        [AllowNull]
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
