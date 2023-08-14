using SAO.UnidadMedidas;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.UnidadMedidas
{
    public class IndexModel : AbpPageModel
    {
        public string? AbreviaturaFilter { get; set; }
        public string? NombreUnidadFilter { get; set; }

        private readonly IUnidadMedidasAppService _unidadMedidasAppService;

        public IndexModel(IUnidadMedidasAppService unidadMedidasAppService)
        {
            _unidadMedidasAppService = unidadMedidasAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}