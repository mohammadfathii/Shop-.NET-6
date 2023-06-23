using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Web.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; } = 0;
        public int DiscountPercent { get; set; } = 0;
        public DateTime ExpireTime { get; set; }
    }
}
