using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.TipoEnvases;

namespace SAO.Web.Pages.TipoEnvases
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public TipoEnvaseUpdateViewModel TipoEnvase { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public EditModalModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;

            TipoEnvase = new();
        }

        public async Task OnGetAsync()
        {
            var tipoEnvase = await _tipoEnvasesAppService.GetAsync(Id);
            TipoEnvase = ObjectMapper.Map<TipoEnvaseDto, TipoEnvaseUpdateViewModel>(tipoEnvase);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _tipoEnvasesAppService.UpdateAsync(Id, ObjectMapper.Map<TipoEnvaseUpdateViewModel, TipoEnvaseUpdateDto>(TipoEnvase));
            return NoContent();
        }
    }

    public class TipoEnvaseUpdateViewModel : TipoEnvaseUpdateDto
    {
    }
}