using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Areas.User.Models.ViewModel
{
    public class CreateUpdateUserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [AllowNull]
        public string Avatar { get; set; }
        [MaybeNull]
        public IFormFile Image { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
