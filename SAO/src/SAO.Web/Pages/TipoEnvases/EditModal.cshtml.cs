using Microsoft.AspNetCore.Mvc;
using SAO.TipoEnvases;
using System.Threading.Tasks;

namespace SAO.Web.Pages.TipoEnvases
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public TipoEnvaseUpdateViewModel TipoEnvase { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public EditModalModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;

            TipoEnvase = new();
        }

        public async Task OnGetAsync()
        {
            var tipoEnvase = await _tipoEnvasesAppService.GetAsync(Id);
            TipoEnvase = ObjectMapper.Map<TipoEnvaseDto, TipoEnvaseUpdateViewModel>(tipoEnvase);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _tipoEnvasesAppService.UpdateAsync(Id, ObjectMapper.Map<TipoEnvaseUpdateViewModel, TipoEnvaseUpdateDto>(TipoEnvase));
            return NoContent();
        }
    }

    public class TipoEnvaseUpdateViewModel : TipoEnvaseUpdateDto
    {
    }
}