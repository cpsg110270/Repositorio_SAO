using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.SustanciaElementals;
using SAO.Shared;

namespace SAO.Web.Pages.SustanciaElementals
{
    public class IndexModel : AbpPageModel
    {
        public string? CodCasFilter { get; set; }
        public string? DesSustanciaFilter { get; set; }

        private readonly ISustanciaElementalsAppService _sustanciaElementalsAppService;

        public IndexModel(ISustanciaElementalsAppService sustanciaElementalsAppService)
        {
            _sustanciaElementalsAppService = sustanciaElementalsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}