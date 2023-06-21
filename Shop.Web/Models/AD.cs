using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Web.Models
{
    public class AD
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        [AllowNull]
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime ExpireTime { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        
        public Category Category { get; set; }
    }
}
