using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.TipoProductos;

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