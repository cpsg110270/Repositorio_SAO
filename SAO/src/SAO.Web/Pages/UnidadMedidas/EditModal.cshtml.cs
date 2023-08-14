using Microsoft.AspNetCore.Mvc;
using SAO.UnidadMedidas;
using System.Threading.Tasks;

namespace SAO.Web.Pages.UnidadMedidas
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public UnidadMedidaUpdateViewModel UnidadMedida { get; set; }

        private readonly IUnidadMedidasAppService _unidadMedidasAppService;

        public EditModalModel(IUnidadMedidasAppService unidadMedidasAppService)
        {
            _unidadMedidasAppService = unidadMedidasAppService;

            UnidadMedida = new();
        }

        public async Task OnGetAsync()
        {
            var unidadMedida = await _unidadMedidasAppService.GetAsync(Id);
            UnidadMedida = ObjectMapper.Map<UnidadMedidaDto, UnidadMedidaUpdateViewModel>(unidadMedida);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _unidadMedidasAppService.UpdateAsync(Id, ObjectMapper.Map<UnidadMedidaUpdateViewModel, UnidadMedidaUpdateDto>(UnidadMedida));
            return NoContent();
        }
    }

    public class UnidadMedidaUpdateViewModel : UnidadMedidaUpdateDto
    {
    }
}