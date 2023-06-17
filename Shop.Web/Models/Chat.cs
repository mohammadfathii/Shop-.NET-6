using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsFinally { get; set; } = false;
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
