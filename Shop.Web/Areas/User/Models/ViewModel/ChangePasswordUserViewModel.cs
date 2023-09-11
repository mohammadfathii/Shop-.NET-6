using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Areas.User.Models.ViewModel
{
    public class ChangePasswordUserViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
