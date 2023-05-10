using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.Importadors;

namespace SAO.Web.Pages.Importadors
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ImportadorUpdateViewModel Importador { get; set; }

        private readonly IImportadorsAppService _importadorsAppService;

        public EditModalModel(IImportadorsAppService importadorsAppService)
        {
            _importadorsAppService = importadorsAppService;

            Importador = new();
        }

        public async Task OnGetAsync()
        {
            var importador = await _importadorsAppService.GetAsync(Id);
            Importador = ObjectMapper.Map<ImportadorDto, ImportadorUpdateViewModel>(importador);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _importadorsAppService.UpdateAsync(Id, ObjectMapper.Map<ImportadorUpdateViewModel, ImportadorUpdateDto>(Importador));
            return NoContent();
        }
    }

    public class ImportadorUpdateViewModel : ImportadorUpdateDto
    {
    }
}