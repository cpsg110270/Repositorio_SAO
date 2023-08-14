using Microsoft.AspNetCore.Mvc;
using SAO.Asraes;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Asraes
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public AsraeUpdateViewModel Asrae { get; set; }

        private readonly IAsraesAppService _asraesAppService;

        public EditModalModel(IAsraesAppService asraesAppService)
        {
            _asraesAppService = asraesAppService;

            Asrae = new();
        }

        public async Task OnGetAsync()
        {
            var asrae = await _asraesAppService.GetAsync(Id);
            Asrae = ObjectMapper.Map<AsraeDto, AsraeUpdateViewModel>(asrae);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _asraesAppService.UpdateAsync(Id, ObjectMapper.Map<AsraeUpdateViewModel, AsraeUpdateDto>(Asrae));
            return NoContent();
        }
    }

    public class AsraeUpdateViewModel : AsraeUpdateDto
    {
    }
}