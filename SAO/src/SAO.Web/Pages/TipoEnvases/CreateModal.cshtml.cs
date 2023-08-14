using Microsoft.AspNetCore.Mvc;
using SAO.TipoEnvases;
using System.Threading.Tasks;

namespace SAO.Web.Pages.TipoEnvases
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public TipoEnvaseCreateViewModel TipoEnvase { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public CreateModalModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;

            TipoEnvase = new();
        }

        public async Task OnGetAsync()
        {
            TipoEnvase = new TipoEnvaseCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _tipoEnvasesAppService.CreateAsync(ObjectMapper.Map<TipoEnvaseCreateViewModel, TipoEnvaseCreateDto>(TipoEnvase));
            return NoContent();
        }
    }

    public class TipoEnvaseCreateViewModel : TipoEnvaseCreateDto
    {
    }
}