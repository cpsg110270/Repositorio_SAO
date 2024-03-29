using Microsoft.AspNetCore.Mvc.Rendering;
using SAO.Productos;
using SAO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.Productos
{
    public class IndexModel : AbpPageModel
    {
        public int? NoProducto { get; set; }
        public string? NombreComerciaFilter { get; set; }
        public string? UsoFilter { get; set; }
        [SelectItems(nameof(FabricanteLookupList))]
        public Guid FabricanteIdFilter { get; set; }
        public List<SelectListItem> FabricanteLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(AsraeLookupList))]
        public int AsraeIdFilter { get; set; }
        public List<SelectListItem> AsraeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(TipoProductoLookupList))]
        public Guid? TipoProductoIdFilter { get; set; }
        public List<SelectListItem> TipoProductoLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IProductosAppService _productosAppService;

        public IndexModel(IProductosAppService productosAppService)
        {
            _productosAppService = productosAppService;
        }

        public async Task OnGetAsync()
        {
            FabricanteLookupList.AddRange((
                    await _productosAppService.GetFabricanteLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            AsraeLookupList.AddRange((
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
    }
}