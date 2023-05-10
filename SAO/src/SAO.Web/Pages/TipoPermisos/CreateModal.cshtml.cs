using SAO.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAO.TipoPermisos;

namespace SAO.Web.Pages.TipoPermisos
{
    public class CreateModalModel : SAOPageModel
    {
        [BindProperty]
        public TipoPermisoCreateViewModel TipoPermiso { get; set; }

        private readonly ITipoPermisosAppService _tipoPermisosAppService;

        public CreateModalModel(ITipoPermisosAppService tipoPermisosAppService)
        {
            _tipoPermisosAppService = tipoPermisosAppService;

            TipoPermiso = new();
        }

        public async Task OnGetAsync()
        {
            TipoPermiso = new TipoPermisoCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _tipoPermisosAppService.CreateAsync(ObjectMapper.Map<TipoPermisoCreateViewModel, TipoPermisoCreateDto>(TipoPermiso));
            return NoContent();
        }
    }

    public class TipoPermisoCreateViewModel : TipoPermisoCreateDto
    {
    }
}