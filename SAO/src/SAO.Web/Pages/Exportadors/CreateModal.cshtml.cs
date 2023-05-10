using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Exportadors;

namespace SAO.Web.Pages.Exportadors
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public ExportadorCreateViewModel Exportador { get; set; }

        private readonly IExportadorsAppService _exportadorsAppService;

        public CreateModalModel(IExportadorsAppService exportadorsAppService)
        {
            _exportadorsAppService = exportadorsAppService;

            Exportador = new();
        }

        public async Task OnGetAsync()
        {
            Exportador = new ExportadorCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _exportadorsAppService.CreateAsync(ObjectMapper.Map<ExportadorCreateViewModel, ExportadorCreateDto>(Exportador));
            return NoContent();
        }
    }

    public class ExportadorCreateViewModel : ExportadorCreateDto
    {
    }
}