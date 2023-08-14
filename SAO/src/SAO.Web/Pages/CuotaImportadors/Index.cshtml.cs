using Microsoft.AspNetCore.Mvc;
using SAO.CuotaImportadors;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.CuotaImportadors
{
    public class IndexModel : AbpPageModel
    {

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ImportadorId { get; set; }


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
            ViewData["ImportId"] = ImportadorId;


            await Task.CompletedTask;
        }
    }
}