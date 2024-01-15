using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.TotalImportacioness;

namespace SAO.Web.Pages.TotalImportacioness
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public TotalImportacionesCreateViewModel TotalImportaciones { get; set; }

        public List<SelectListItem> TipoProductoLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ITotalImportacionessAppService _totalImportacionessAppService;

        public CreateModalModel(ITotalImportacionessAppService totalImportacionessAppService)
        {
            _totalImportacionessAppService = totalImportacionessAppService;

            TotalImportaciones = new();
        }

        public async Task OnGetAsync()
        {
            TotalImportaciones = new TotalImportacionesCreateViewModel();
            TipoProductoLookupListRequired.AddRange((
                                    await _totalImportacionessAppService.GetTipoProductoLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _totalImportacionessAppService.CreateAsync(ObjectMapper.Map<TotalImportacionesCreateViewModel, TotalImportacionesCreateDto>(TotalImportaciones));
            return NoContent();
        }
    }

    public class TotalImportacionesCreateViewModel : TotalImportacionesCreateDto
    {
    }
}