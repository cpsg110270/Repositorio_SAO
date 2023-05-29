using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.CuotaImportadors;

namespace SAO.Web.Pages.CuotaImportadors
{
    public class CreateModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ImportadorId { get; set; }

        [BindProperty]
        public CuotaImportadorCreateViewModel CuotaImportador { get; set; }

        private readonly ICuotaImportadorsAppService _cuotaImportadorsAppService;

        public CreateModalModel(ICuotaImportadorsAppService cuotaImportadorsAppService)
        {
            _cuotaImportadorsAppService = cuotaImportadorsAppService;

            CuotaImportador = new();
        }

        public async Task OnGetAsync()
        {
            CuotaImportador = new CuotaImportadorCreateViewModel();
            CuotaImportador.ImportadorId = ImportadorId;

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _cuotaImportadorsAppService.CreateAsync(ObjectMapper.Map<CuotaImportadorCreateViewModel, CuotaImportadorCreateDto>(CuotaImportador));
            return NoContent();
        }
    }

    public class CuotaImportadorCreateViewModel : CuotaImportadorCreateDto
    {
    }
}