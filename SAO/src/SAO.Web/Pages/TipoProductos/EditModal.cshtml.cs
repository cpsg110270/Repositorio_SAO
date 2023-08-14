using Microsoft.AspNetCore.Mvc;
using SAO.TipoProductos;
using System;
using System.Threading.Tasks;

namespace SAO.Web.Pages.TipoProductos
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public TipoProductoUpdateViewModel TipoProducto { get; set; }

        private readonly ITipoProductosAppService _tipoProductosAppService;

        public EditModalModel(ITipoProductosAppService tipoProductosAppService)
        {
            _tipoProductosAppService = tipoProductosAppService;

            TipoProducto = new();
        }

        public async Task OnGetAsync()
        {
            var tipoProducto = await _tipoProductosAppService.GetAsync(Id);
            TipoProducto = ObjectMapper.Map<TipoProductoDto, TipoProductoUpdateViewModel>(tipoProducto);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _tipoProductosAppService.UpdateAsync(Id, ObjectMapper.Map<TipoProductoUpdateViewModel, TipoProductoUpdateDto>(TipoProducto));
            return NoContent();
        }
    }

    public class TipoProductoUpdateViewModel : TipoProductoUpdateDto
    {
    }
}