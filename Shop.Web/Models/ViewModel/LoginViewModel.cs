using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models.ViewModel
{
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
