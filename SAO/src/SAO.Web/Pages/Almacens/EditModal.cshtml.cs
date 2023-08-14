using Microsoft.AspNetCore.Mvc;
using SAO.Almacens;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Almacens
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public AlmacenUpdateViewModel Almacen { get; set; }

        private readonly IAlmacensAppService _almacensAppService;

        public EditModalModel(IAlmacensAppService almacensAppService)
        {
            _almacensAppService = almacensAppService;

            Almacen = new();
        }

        public async Task OnGetAsync()
        {
            var almacen = await _almacensAppService.GetAsync(Id);
            Almacen = ObjectMapper.Map<AlmacenDto, AlmacenUpdateViewModel>(almacen);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _almacensAppService.UpdateAsync(Id, ObjectMapper.Map<AlmacenUpdateViewModel, AlmacenUpdateDto>(Almacen));
            return NoContent();
        }
    }

    public class AlmacenUpdateViewModel : AlmacenUpdateDto
    {
    }
}