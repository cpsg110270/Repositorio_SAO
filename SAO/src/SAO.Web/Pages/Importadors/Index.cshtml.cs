using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.Importadors;
using SAO.Shared;

namespace SAO.Web.Pages.Importadors
{
    public class IndexModel : AbpPageModel
    {
        public string? NombreImportadorFilter { get; set; }

        private readonly IImportadorsAppService _importadorsAppService;

        public IndexModel(IImportadorsAppService importadorsAppService)
        {
            _importadorsAppService = importadorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}