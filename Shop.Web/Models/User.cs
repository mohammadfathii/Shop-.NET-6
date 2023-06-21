using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Shop.Web.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [AllowNull]
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; } = false;
        [AllowNull]
        public string VerifyToken { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string IPAddress { get; set; }
        public DateTime LastLoginTime { get; set; }


        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<SubComment> SubComments { get; set; }

    }
}
