using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.PuertoEntradaSalidas;
using SAO.Shared;

namespace SAO.Web.Pages.PuertoEntradaSalidas
{
    public class IndexModel : AbpPageModel
    {
        public string? NombrePuertoFilter { get; set; }

        private readonly IPuertoEntradaSalidasAppService _puertoEntradaSalidasAppService;

        public IndexModel(IPuertoEntradaSalidasAppService puertoEntradaSalidasAppService)
        {
            _puertoEntradaSalidasAppService = puertoEntradaSalidasAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}