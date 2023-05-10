using SAO.Shared;
using SAO.SustanciaElementals;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.Productos;

namespace SAO.Web.Pages.Productos
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ProductoUpdateViewModel Producto { get; set; }

        public List<SustanciaElementalDto> SustanciaElementals { get; set; }
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

        public EditModalModel(IProductosAppService productosAppService)
        {
            _productosAppService = productosAppService;

            Producto = new();
        }

        public async Task OnGetAsync()
        {
            var productoWithNavigationPropertiesDto = await _productosAppService.GetWithNavigationPropertiesAsync(Id);
            Producto = ObjectMapper.Map<ProductoDto, ProductoUpdateViewModel>(productoWithNavigationPropertiesDto.Producto);

            SustanciaElementals = productoWithNavigationPropertiesDto.SustanciaElementals;
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

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            Producto.SustanciaElementalIds = SelectedSustanciaElementalIds;

            await _productosAppService.UpdateAsync(Id, ObjectMapper.Map<ProductoUpdateViewModel, ProductoUpdateDto>(Producto));
            return NoContent();
        }
    }

    public class ProductoUpdateViewModel : ProductoUpdateDto
    {
    }
}