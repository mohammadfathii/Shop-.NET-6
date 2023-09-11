using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Models.ViewModel
{
    public class CreateUpdateProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; } = 10000;
        public int QuantityInStock { get; set; }
        [AllowNull]
        [Range(0, 99)]
        public int DiscountPercent { get; set; } = 0;
        [AllowNull]
        [Range(0,9999999)]
        public int DiscountCount { get; set; } = 0;
        [MaybeNull]
        public IFormFile Thumbnail { get; set; }
        [AllowNull]
        public string ThumbnailIMG { get; set; } = string.Empty;
        [AllowNull]
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
