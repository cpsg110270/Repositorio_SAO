using Microsoft.AspNetCore.Mvc;
using SAO.SustanciaElementals;
using System.Threading.Tasks;

namespace SAO.Web.Pages.SustanciaElementals
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public SustanciaElementalCreateViewModel SustanciaElemental { get; set; }

        private readonly ISustanciaElementalsAppService _sustanciaElementalsAppService;

        public CreateModalModel(ISustanciaElementalsAppService sustanciaElementalsAppService)
        {
            _sustanciaElementalsAppService = sustanciaElementalsAppService;

            SustanciaElemental = new();
        }

        public async Task OnGetAsync()
        {
            SustanciaElemental = new SustanciaElementalCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _sustanciaElementalsAppService.CreateAsync(ObjectMapper.Map<SustanciaElementalCreateViewModel, SustanciaElementalCreateDto>(SustanciaElemental));
            return NoContent();
        }
    }

    public class SustanciaElementalCreateViewModel : SustanciaElementalCreateDto
    {
    }
}