using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Importadors;

namespace SAO.Web.Pages.Importadors
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public ImportadorCreateViewModel Importador { get; set; }

        private readonly IImportadorsAppService _importadorsAppService;

        public CreateModalModel(IImportadorsAppService importadorsAppService)
        {
            _importadorsAppService = importadorsAppService;

            Importador = new();
        }

        public async Task OnGetAsync()
        {
            Importador = new ImportadorCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _importadorsAppService.CreateAsync(ObjectMapper.Map<ImportadorCreateViewModel, ImportadorCreateDto>(Importador));
            return NoContent();
        }
    }

    public class ImportadorCreateViewModel : ImportadorCreateDto
    {
    }
}