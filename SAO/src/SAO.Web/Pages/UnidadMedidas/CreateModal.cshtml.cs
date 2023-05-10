using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.UnidadMedidas;

namespace SAO.Web.Pages.UnidadMedidas
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public UnidadMedidaCreateViewModel UnidadMedida { get; set; }

        private readonly IUnidadMedidasAppService _unidadMedidasAppService;

        public CreateModalModel(IUnidadMedidasAppService unidadMedidasAppService)
        {
            _unidadMedidasAppService = unidadMedidasAppService;

            UnidadMedida = new();
        }

        public async Task OnGetAsync()
        {
            UnidadMedida = new UnidadMedidaCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _unidadMedidasAppService.CreateAsync(ObjectMapper.Map<UnidadMedidaCreateViewModel, UnidadMedidaCreateDto>(UnidadMedida));
            return NoContent();
        }
    }

    public class UnidadMedidaCreateViewModel : UnidadMedidaCreateDto
    {
    }
}