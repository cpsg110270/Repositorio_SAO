using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.Exportadors;
using SAO.Shared;

namespace SAO.Web.Pages.Exportadors
{
    public class IndexModel : AbpPageModel
    {
        public string? NombreExportadorFilter { get; set; }

        private readonly IExportadorsAppService _exportadorsAppService;

        public IndexModel(IExportadorsAppService exportadorsAppService)
        {
            _exportadorsAppService = exportadorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}