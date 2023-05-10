using SAO.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.PuertoEntradaSalidas;

namespace SAO.Web.Pages.PuertoEntradaSalidas
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public PuertoEntradaSalidaUpdateViewModel PuertoEntradaSalida { get; set; }

        private readonly IPuertoEntradaSalidasAppService _puertoEntradaSalidasAppService;

        public EditModalModel(IPuertoEntradaSalidasAppService puertoEntradaSalidasAppService)
        {
            _puertoEntradaSalidasAppService = puertoEntradaSalidasAppService;

            PuertoEntradaSalida = new();
        }

        public async Task OnGetAsync()
        {
            var puertoEntradaSalida = await _puertoEntradaSalidasAppService.GetAsync(Id);
            PuertoEntradaSalida = ObjectMapper.Map<PuertoEntradaSalidaDto, PuertoEntradaSalidaUpdateViewModel>(puertoEntradaSalida);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _puertoEntradaSalidasAppService.UpdateAsync(Id, ObjectMapper.Map<PuertoEntradaSalidaUpdateViewModel, PuertoEntradaSalidaUpdateDto>(PuertoEntradaSalida));
            return NoContent();
        }
    }

    public class PuertoEntradaSalidaUpdateViewModel : PuertoEntradaSalidaUpdateDto
    {
    }
}