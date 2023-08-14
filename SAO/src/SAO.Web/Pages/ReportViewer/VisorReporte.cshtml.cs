using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace SAO.Web.Pages.ReportViewer
//{
//    public class IndexModel : PageModel
//    {
//        public void OnGet()
//        {
//        }
//    }
//}
namespace SAO.Web.Pages.ReportViewer
{
    public class VisorReporteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ReportPathName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        public void OnGet()
        {
        }
    }
}
