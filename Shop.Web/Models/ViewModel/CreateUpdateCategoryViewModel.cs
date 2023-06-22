using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Shop.Web.Models.ViewModel
{
    public class CreateUpdateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
