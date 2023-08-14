using Microsoft.AspNetCore.Mvc;
using SAO.Asraes;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Asraes
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public AsraeCreateViewModel Asrae { get; set; }

        private readonly IAsraesAppService _asraesAppService;

        public CreateModalModel(IAsraesAppService asraesAppService)
        {
            _asraesAppService = asraesAppService;

            Asrae = new();
        }

        public async Task OnGetAsync()
        {
            Asrae = new AsraeCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _asraesAppService.CreateAsync(ObjectMapper.Map<AsraeCreateViewModel, AsraeCreateDto>(Asrae));
            return NoContent();
        }
    }

    public class AsraeCreateViewModel : AsraeCreateDto
    {
    }
}