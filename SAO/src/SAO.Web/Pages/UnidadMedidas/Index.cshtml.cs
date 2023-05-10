using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.UnidadMedidas;
using SAO.Shared;

namespace SAO.Web.Pages.UnidadMedidas
{
    public class IndexModel : AbpPageModel
    {
        public string? AbreviaturaFilter { get; set; }
        public string? NombreUnidadFilter { get; set; }

        private readonly IUnidadMedidasAppService _unidadMedidasAppService;

        public IndexModel(IUnidadMedidasAppService unidadMedidasAppService)
        {
            _unidadMedidasAppService = unidadMedidasAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}