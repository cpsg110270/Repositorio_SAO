using Microsoft.AspNetCore.Mvc;
using SAO.TipoProductos;
using System.Threading.Tasks;

namespace SAO.Web.Pages.TipoProductos
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public TipoProductoCreateViewModel TipoProducto { get; set; }

        private readonly ITipoProductosAppService _tipoProductosAppService;

        public CreateModalModel(ITipoProductosAppService tipoProductosAppService)
        {
            _tipoProductosAppService = tipoProductosAppService;

            TipoProducto = new();
        }

        public async Task OnGetAsync()
        {
            TipoProducto = new TipoProductoCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _tipoProductosAppService.CreateAsync(ObjectMapper.Map<TipoProductoCreateViewModel, TipoProductoCreateDto>(TipoProducto));
            return NoContent();
        }
    }

    public class TipoProductoCreateViewModel : TipoProductoCreateDto
    {
    }
}