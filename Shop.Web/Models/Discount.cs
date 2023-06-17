using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; } = 0;
        public int DiscountPercent { get; set; } = 0;
        public DateTime ExpireTime { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
