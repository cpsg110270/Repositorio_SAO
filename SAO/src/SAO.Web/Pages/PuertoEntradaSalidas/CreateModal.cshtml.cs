using Microsoft.AspNetCore.Mvc;
using SAO.PuertoEntradaSalidas;
using System.Threading.Tasks;

namespace SAO.Web.Pages.PuertoEntradaSalidas
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public PuertoEntradaSalidaCreateViewModel PuertoEntradaSalida { get; set; }

        private readonly IPuertoEntradaSalidasAppService _puertoEntradaSalidasAppService;

        public CreateModalModel(IPuertoEntradaSalidasAppService puertoEntradaSalidasAppService)
        {
            _puertoEntradaSalidasAppService = puertoEntradaSalidasAppService;

            PuertoEntradaSalida = new();
        }

        public async Task OnGetAsync()
        {
            PuertoEntradaSalida = new PuertoEntradaSalidaCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _puertoEntradaSalidasAppService.CreateAsync(ObjectMapper.Map<PuertoEntradaSalidaCreateViewModel, PuertoEntradaSalidaCreateDto>(PuertoEntradaSalida));
            return NoContent();
        }
    }

    public class PuertoEntradaSalidaCreateViewModel : PuertoEntradaSalidaCreateDto
    {
    }
}