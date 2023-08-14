using SAO.PuertoEntradaSalidas;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.PuertoEntradaSalidas
{
    public class IndexModel : AbpPageModel
    {
        public string? NombrePuertoFilter { get; set; }

        private readonly IPuertoEntradaSalidasAppService _puertoEntradaSalidasAppService;

        public IndexModel(IPuertoEntradaSalidasAppService puertoEntradaSalidasAppService)
        {
            _puertoEntradaSalidasAppService = puertoEntradaSalidasAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}