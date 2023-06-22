using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Models.ViewModel
{
    public class CreateUpdateProductViewModel
    {
        [AllowNull]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; } = 100000;
        [AllowNull]
        [Range(0,99)]
        public int DiscountPercent { get; set; }
        [AllowNull]
        [Range(1,9999999)]
        public int DiscountCount { get; set; }
        [Required]
        public IFormFile Thumbnail { get; set; }


        public List<Category> Categories { get; set; }

    }
}
