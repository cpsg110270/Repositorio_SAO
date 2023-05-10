using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.TipoProductos;

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