using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ReportMessage
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int ReportId { get; set; }

        public Report Report { get; set; }
    }
}
