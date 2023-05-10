using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.TipoPermisos;
using SAO.Shared;

namespace SAO.Web.Pages.TipoPermisos
{
    public class IndexModel : AbpPageModel
    {
        public string? CodigoFilter { get; set; }
        public string? DesripcionFilter { get; set; }

        private readonly ITipoPermisosAppService _tipoPermisosAppService;

        public IndexModel(ITipoPermisosAppService tipoPermisosAppService)
        {
            _tipoPermisosAppService = tipoPermisosAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}