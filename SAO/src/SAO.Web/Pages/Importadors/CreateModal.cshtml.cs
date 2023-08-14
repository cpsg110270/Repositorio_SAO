using Microsoft.AspNetCore.Mvc;
using SAO.Importadors;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Importadors
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public ImportadorCreateViewModel Importador { get; set; }

        private readonly IImportadorsAppService _importadorsAppService;

        public CreateModalModel(IImportadorsAppService importadorsAppService)
        {
            _importadorsAppService = importadorsAppService;

            Importador = new();
        }

        public async Task OnGetAsync()
        {
            Importador = new ImportadorCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _importadorsAppService.CreateAsync(ObjectMapper.Map<ImportadorCreateViewModel, ImportadorCreateDto>(Importador));
            return NoContent();
        }
    }

    public class ImportadorCreateViewModel : ImportadorCreateDto
    {
    }
}