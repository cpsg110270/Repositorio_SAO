using Microsoft.AspNetCore.Mvc;
using SAO.CuotaImportadors;
using SAO.Importadors;
using System;
using System.Threading.Tasks;

namespace SAO.Web.Pages.CuotaImportadors
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CuotaImportadorUpdateViewModel CuotaImportador { get; set; }

        public ImportadorDto Importador { get; set; }

        private readonly ICuotaImportadorsAppService _cuotaImportadorsAppService;

        public EditModalModel(ICuotaImportadorsAppService cuotaImportadorsAppService)
        {
            _cuotaImportadorsAppService = cuotaImportadorsAppService;

            CuotaImportador = new();
        }

        public async Task OnGetAsync()
        {
            var cuotaImportadorWithNavigationPropertiesDto = await _cuotaImportadorsAppService.GetWithNavigationPropertiesAsync(Id);
            CuotaImportador = ObjectMapper.Map<CuotaImportadorDto, CuotaImportadorUpdateViewModel>(cuotaImportadorWithNavigationPropertiesDto.CuotaImportador);

            Importador = cuotaImportadorWithNavigationPropertiesDto.Importador;

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _cuotaImportadorsAppService.UpdateAsync(Id, ObjectMapper.Map<CuotaImportadorUpdateViewModel, CuotaImportadorUpdateDto>(CuotaImportador));
            return NoContent();
        }
    }

    public class CuotaImportadorUpdateViewModel : CuotaImportadorUpdateDto
    {
    }
}