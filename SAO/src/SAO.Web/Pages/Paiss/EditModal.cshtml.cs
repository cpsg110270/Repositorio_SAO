using Microsoft.AspNetCore.Mvc;
using SAO.Paiss;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Paiss
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public PaisUpdateViewModel Pais { get; set; }

        private readonly IPaissAppService _paissAppService;

        public EditModalModel(IPaissAppService paissAppService)
        {
            _paissAppService = paissAppService;

            Pais = new();
        }

        public async Task OnGetAsync()
        {
            var pais = await _paissAppService.GetAsync(Id);
            Pais = ObjectMapper.Map<PaisDto, PaisUpdateViewModel>(pais);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _paissAppService.UpdateAsync(Id, ObjectMapper.Map<PaisUpdateViewModel, PaisUpdateDto>(Pais));
            return NoContent();
        }
    }

    public class PaisUpdateViewModel : PaisUpdateDto
    {
    }
}