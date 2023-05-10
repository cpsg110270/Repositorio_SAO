using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Productos;

namespace SAO.Web.Pages.Productos
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public ProductoCreateViewModel Producto { get; set; }

        [BindProperty]
        public List<Guid> SelectedSustanciaElementalIds { get; set; }
        public List<SelectListItem> FabricanteLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> AsraeLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> TipoProductoLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IProductosAppService _productosAppService;

        public CreateModalModel(IProductosAppService productosAppService)
        {
            _productosAppService = productosAppService;

            Producto = new();
        }

        public async Task OnGetAsync()
        {
            Producto = new ProductoCreateViewModel();
            FabricanteLookupListRequired.AddRange((
                                    await _productosAppService.GetFabricanteLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            AsraeLookupListRequired.AddRange((
                                    await _productosAppService.GetAsraeLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            TipoProductoLookupList.AddRange((
                                    await _productosAppService.GetTipoProductoLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Producto.SustanciaElementalIds = SelectedSustanciaElementalIds;

            await _productosAppService.CreateAsync(ObjectMapper.Map<ProductoCreateViewModel, ProductoCreateDto>(Producto));
            return NoContent();
        }
    }

    public class ProductoCreateViewModel : ProductoCreateDto
    {
    }
}