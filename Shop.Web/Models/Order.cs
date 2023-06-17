using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool isFinally { get; set; } = false;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
