using SAO.Fabricantes;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages.Fabricantes
{
    public class IndexModel : AbpPageModel
    {
        public string? NombreFabricanteFilter { get; set; }

        private readonly IFabricantesAppService _fabricantesAppService;

        public IndexModel(IFabricantesAppService fabricantesAppService)
        {
            _fabricantesAppService = fabricantesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}