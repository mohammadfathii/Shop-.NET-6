using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
    }
}
