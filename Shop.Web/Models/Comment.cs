using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<SubComment> SubComments { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
