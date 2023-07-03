using Microsoft.Build.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Models.ViewModel
{
    public class CreateUpdateAdsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile Thumbnail { get; set; } = null;
        public string Image { get; set; }
        public string URL { get; set; }
        public int CategoryId { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
