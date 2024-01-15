using SAO.Shared;
using SAO.Asraes;
using SAO.Importadors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.TotalImportacioness;

namespace SAO.Web.Pages.TotalImportacioness
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public TotalImportacionesUpdateViewModel TotalImportaciones { get; set; }

        public ImportadorDto Importador { get; set; }
        public AsraeDto Asrae { get; set; }
        public List<SelectListItem> TipoProductoLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ITotalImportacionessAppService _totalImportacionessAppService;

        public EditModalModel(ITotalImportacionessAppService totalImportacionessAppService)
        {
            _totalImportacionessAppService = totalImportacionessAppService;

            TotalImportaciones = new();
        }

        public async Task OnGetAsync()
        {
            var totalImportacionesWithNavigationPropertiesDto = await _totalImportacionessAppService.GetWithNavigationPropertiesAsync(Id);
            TotalImportaciones = ObjectMapper.Map<TotalImportacionesDto, TotalImportacionesUpdateViewModel>(totalImportacionesWithNavigationPropertiesDto.TotalImportaciones);

            Importador = totalImportacionesWithNavigationPropertiesDto.Importador;
            Asrae = totalImportacionesWithNavigationPropertiesDto.Asrae;
            TipoProductoLookupListRequired.AddRange((
                        await _totalImportacionessAppService.GetTipoProductoLookupAsync(new LookupRequestDto
                        {
                            MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                        })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _totalImportacionessAppService.UpdateAsync(Id, ObjectMapper.Map<TotalImportacionesUpdateViewModel, TotalImportacionesUpdateDto>(TotalImportaciones));
            return NoContent();
        }
    }

    public class TotalImportacionesUpdateViewModel : TotalImportacionesUpdateDto
    {
    }
}