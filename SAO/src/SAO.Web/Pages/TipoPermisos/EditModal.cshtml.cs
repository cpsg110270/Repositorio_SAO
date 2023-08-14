using Microsoft.AspNetCore.Mvc;
using SAO.TipoPermisos;
using System;
using System.Threading.Tasks;

namespace SAO.Web.Pages.TipoPermisos
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public TipoPermisoUpdateViewModel TipoPermiso { get; set; }

        private readonly ITipoPermisosAppService _tipoPermisosAppService;

        public EditModalModel(ITipoPermisosAppService tipoPermisosAppService)
        {
            _tipoPermisosAppService = tipoPermisosAppService;

            TipoPermiso = new();
        }

        public async Task OnGetAsync()
        {
            var tipoPermiso = await _tipoPermisosAppService.GetAsync(Id);
            TipoPermiso = ObjectMapper.Map<TipoPermisoDto, TipoPermisoUpdateViewModel>(tipoPermiso);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _tipoPermisosAppService.UpdateAsync(Id, ObjectMapper.Map<TipoPermisoUpdateViewModel, TipoPermisoUpdateDto>(TipoPermiso));
            return NoContent();
        }
    }

    public class TipoPermisoUpdateViewModel : TipoPermisoUpdateDto
    {
    }
}