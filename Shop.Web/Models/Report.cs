using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public bool IsFinally { get; set; } = false;
        public User User { get; set; }
        public ICollection<ReportMessage> ReportMessages { get; set; }
    }
}
