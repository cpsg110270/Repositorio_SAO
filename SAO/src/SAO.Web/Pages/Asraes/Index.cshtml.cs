using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.Asraes;
using SAO.Shared;

namespace SAO.Web.Pages.Asraes
{
    public class IndexModel : AbpPageModel
    {
        public string? Codigo_ASHRAEFilter { get; set; }
        public string? DescripcionFilter { get; set; }

        private readonly IAsraesAppService _asraesAppService;

        public IndexModel(IAsraesAppService asraesAppService)
        {
            _asraesAppService = asraesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}