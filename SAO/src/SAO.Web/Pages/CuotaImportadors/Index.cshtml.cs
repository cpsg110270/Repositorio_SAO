using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.CuotaImportadors;
using SAO.Shared;

namespace SAO.Web.Pages.CuotaImportadors
{
    public class IndexModel : AbpPageModel
    {
        public int? AñoFilterMin { get; set; }

        public int? AñoFilterMax { get; set; }
        public decimal? CuotaFilterMin { get; set; }

        public decimal? CuotaFilterMax { get; set; }

        private readonly ICuotaImportadorsAppService _cuotaImportadorsAppService;

        public IndexModel(ICuotaImportadorsAppService cuotaImportadorsAppService)
        {
            _cuotaImportadorsAppService = cuotaImportadorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}