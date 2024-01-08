using SAO.Importadors;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.Importadors
{
    public class IndexModel : AbpPageModel
    {
        public string? NombreImportadorFilter { get; set; }
        public int? NoImportadorFilter { get; set; }

        public string? NoRUCFilter { get; set; }


        private readonly IImportadorsAppService _importadorsAppService;

        public IndexModel(IImportadorsAppService importadorsAppService)
        {
            _importadorsAppService = importadorsAppService;
        }


        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}