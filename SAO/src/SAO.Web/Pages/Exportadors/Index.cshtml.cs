using SAO.Exportadors;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.Exportadors
{
    public class IndexModel : AbpPageModel
    {
        public string? NoImportadorFilter { get; set; }
        public string? NombreExportadorFilter { get; set; }

        private readonly IExportadorsAppService _exportadorsAppService;

        public IndexModel(IExportadorsAppService exportadorsAppService)
        {
            _exportadorsAppService = exportadorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}