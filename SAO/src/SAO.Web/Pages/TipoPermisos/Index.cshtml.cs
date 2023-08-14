using SAO.TipoPermisos;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.TipoPermisos
{
    public class IndexModel : AbpPageModel
    {
        public string? CodigoFilter { get; set; }
        public string? DesripcionFilter { get; set; }

        private readonly ITipoPermisosAppService _tipoPermisosAppService;

        public IndexModel(ITipoPermisosAppService tipoPermisosAppService)
        {
            _tipoPermisosAppService = tipoPermisosAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}