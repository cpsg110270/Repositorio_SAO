using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.Paiss;
using SAO.Shared;

namespace SAO.Web.Pages.Paiss
{
    public class IndexModel : AbpPageModel
    {
        public string? NombrePaisFilter { get; set; }

        private readonly IPaissAppService _paissAppService;

        public IndexModel(IPaissAppService paissAppService)
        {
            _paissAppService = paissAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}