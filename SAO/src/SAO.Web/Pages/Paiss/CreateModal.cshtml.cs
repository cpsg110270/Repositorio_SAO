using Microsoft.AspNetCore.Mvc;
using SAO.Paiss;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Paiss
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public PaisCreateViewModel Pais { get; set; }

        private readonly IPaissAppService _paissAppService;

        public CreateModalModel(IPaissAppService paissAppService)
        {
            _paissAppService = paissAppService;

            Pais = new();
        }

        public async Task OnGetAsync()
        {
            Pais = new PaisCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _paissAppService.CreateAsync(ObjectMapper.Map<PaisCreateViewModel, PaisCreateDto>(Pais));
            return NoContent();
        }
    }

    public class PaisCreateViewModel : PaisCreateDto
    {
    }
}