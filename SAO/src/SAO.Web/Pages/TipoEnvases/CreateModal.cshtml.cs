using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.TipoEnvases;

namespace SAO.Web.Pages.TipoEnvases
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public TipoEnvaseCreateViewModel TipoEnvase { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public CreateModalModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;

            TipoEnvase = new();
        }

        public async Task OnGetAsync()
        {
            TipoEnvase = new TipoEnvaseCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _tipoEnvasesAppService.CreateAsync(ObjectMapper.Map<TipoEnvaseCreateViewModel, TipoEnvaseCreateDto>(TipoEnvase));
            return NoContent();
        }
    }

    public class TipoEnvaseCreateViewModel : TipoEnvaseCreateDto
    {
    }
}