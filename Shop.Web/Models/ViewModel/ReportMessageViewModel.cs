using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models.ViewModel
{
    public class ReportMessageViewModel
    {
        public string Body { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }
}
