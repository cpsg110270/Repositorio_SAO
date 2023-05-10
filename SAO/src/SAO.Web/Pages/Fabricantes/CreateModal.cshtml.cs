using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.Fabricantes;

namespace SAO.Web.Pages.Fabricantes
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public FabricanteCreateViewModel Fabricante { get; set; }

        private readonly IFabricantesAppService _fabricantesAppService;

        public CreateModalModel(IFabricantesAppService fabricantesAppService)
        {
            _fabricantesAppService = fabricantesAppService;

            Fabricante = new();
        }

        public async Task OnGetAsync()
        {
            Fabricante = new FabricanteCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _fabricantesAppService.CreateAsync(ObjectMapper.Map<FabricanteCreateViewModel, FabricanteCreateDto>(Fabricante));
            return NoContent();
        }
    }

    public class FabricanteCreateViewModel : FabricanteCreateDto
    {
    }
}