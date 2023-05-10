using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.Almacens;
using SAO.Shared;

namespace SAO.Web.Pages.Almacens
{
    public class IndexModel : AbpPageModel
    {
        public string? NombreAlmacenFilter { get; set; }
        public string? SiglaAlmacenFilter { get; set; }

        private readonly IAlmacensAppService _almacensAppService;

        public IndexModel(IAlmacensAppService almacensAppService)
        {
            _almacensAppService = almacensAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}