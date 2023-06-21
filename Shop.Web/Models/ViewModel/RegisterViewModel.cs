using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Range(8,40,ErrorMessage = "Password Must Between 8 and 40 characters")]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string RePassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
