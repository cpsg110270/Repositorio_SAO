using Microsoft.AspNetCore.Mvc;
using SAO.Almacens;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Almacens
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public AlmacenCreateViewModel Almacen { get; set; }

        private readonly IAlmacensAppService _almacensAppService;

        public CreateModalModel(IAlmacensAppService almacensAppService)
        {
            _almacensAppService = almacensAppService;

            Almacen = new();
        }

        public async Task OnGetAsync()
        {
            Almacen = new AlmacenCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _almacensAppService.CreateAsync(ObjectMapper.Map<AlmacenCreateViewModel, AlmacenCreateDto>(Almacen));
            return NoContent();
        }
    }

    public class AlmacenCreateViewModel : AlmacenCreateDto
    {
    }
}