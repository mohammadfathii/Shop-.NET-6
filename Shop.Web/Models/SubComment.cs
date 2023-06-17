using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class SubComment
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }

        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
