using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.TipoEnvases;
using SAO.Shared;

namespace SAO.Web.Pages.TipoEnvases
{
    public class IndexModel : AbpPageModel
    {
        public string? DesEnvaseFilter { get; set; }

        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;

        public IndexModel(ITipoEnvasesAppService tipoEnvasesAppService)
        {
            _tipoEnvasesAppService = tipoEnvasesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}