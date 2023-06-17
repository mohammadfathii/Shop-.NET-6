using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Order Orderr { get; set; }
        public Product Product { get; set; }
    }
}
