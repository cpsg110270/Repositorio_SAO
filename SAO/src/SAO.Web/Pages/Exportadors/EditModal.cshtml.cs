using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.Exportadors;

namespace SAO.Web.Pages.Exportadors
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ExportadorUpdateViewModel Exportador { get; set; }

        private readonly IExportadorsAppService _exportadorsAppService;

        public EditModalModel(IExportadorsAppService exportadorsAppService)
        {
            _exportadorsAppService = exportadorsAppService;

            Exportador = new();
        }

        public async Task OnGetAsync()
        {
            var exportador = await _exportadorsAppService.GetAsync(Id);
            Exportador = ObjectMapper.Map<ExportadorDto, ExportadorUpdateViewModel>(exportador);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _exportadorsAppService.UpdateAsync(Id, ObjectMapper.Map<ExportadorUpdateViewModel, ExportadorUpdateDto>(Exportador));
            return NoContent();
        }
    }

    public class ExportadorUpdateViewModel : ExportadorUpdateDto
    {
    }
}