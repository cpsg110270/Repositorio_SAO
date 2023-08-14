using Microsoft.AspNetCore.Mvc;
using SAO.Fabricantes;
using System;
using System.Threading.Tasks;

namespace SAO.Web.Pages.Fabricantes
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public FabricanteUpdateViewModel Fabricante { get; set; }

        private readonly IFabricantesAppService _fabricantesAppService;

        public EditModalModel(IFabricantesAppService fabricantesAppService)
        {
            _fabricantesAppService = fabricantesAppService;

            Fabricante = new();
        }

        public async Task OnGetAsync()
        {
            var fabricante = await _fabricantesAppService.GetAsync(Id);
            Fabricante = ObjectMapper.Map<FabricanteDto, FabricanteUpdateViewModel>(fabricante);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _fabricantesAppService.UpdateAsync(Id, ObjectMapper.Map<FabricanteUpdateViewModel, FabricanteUpdateDto>(Fabricante));
            return NoContent();
        }
    }

    public class FabricanteUpdateViewModel : FabricanteUpdateDto
    {
    }
}