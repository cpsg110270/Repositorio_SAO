using SAO.TipoEnvases;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.TipoEnvases
{
    public class IndexModel : AbpPageModel
    {
        public string? DesEnvaseFilter { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public IndexModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}