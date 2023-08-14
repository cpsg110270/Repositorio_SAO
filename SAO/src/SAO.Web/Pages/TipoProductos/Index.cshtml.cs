using SAO.TipoProductos;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.TipoProductos
{
    public class IndexModel : AbpPageModel
    {
        public string? DesProductoFilter { get; set; }

        private readonly ITipoProductosAppService _tipoProductosAppService;

        public IndexModel(ITipoProductosAppService tipoProductosAppService)
        {
            _tipoProductosAppService = tipoProductosAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}