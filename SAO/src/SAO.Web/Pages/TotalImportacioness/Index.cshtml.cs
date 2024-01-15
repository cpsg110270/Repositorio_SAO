using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.TotalImportacioness;
using SAO.Shared;

namespace SAO.Web.Pages.TotalImportacioness
{
    public class IndexModel : AbpPageModel
    {
        public int? AnioFilterMin { get; set; }

        public int? AnioFilterMax { get; set; }
        public double? CuotaAsignadaFilterMin { get; set; }

        public double? CuotaAsignadaFilterMax { get; set; }
        public double? CuotaConsumidaFilterMin { get; set; }

        public double? CuotaConsumidaFilterMax { get; set; }
        [SelectItems(nameof(ImportadorLookupList))]
        public Guid ImportadorIdFilter { get; set; }
        public List<SelectListItem> ImportadorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(TipoProductoLookupList))]
        public Guid TipoProductoIdFilter { get; set; }
        public List<SelectListItem> TipoProductoLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(AsraeLookupList))]
        public int AsraeIdFilter { get; set; }
        public List<SelectListItem> AsraeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        private readonly ITotalImportacionessAppService _totalImportacionessAppService;

        public IndexModel(ITotalImportacionessAppService totalImportacionessAppService)
        {
            _totalImportacionessAppService = totalImportacionessAppService;
        }

        public async Task OnGetAsync()
        {
            ImportadorLookupList.AddRange((
                    await _totalImportacionessAppService.GetImportadorLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            TipoProductoLookupList.AddRange((
                            await _totalImportacionessAppService.GetTipoProductoLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            AsraeLookupList.AddRange((
                            await _totalImportacionessAppService.GetAsraeLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}