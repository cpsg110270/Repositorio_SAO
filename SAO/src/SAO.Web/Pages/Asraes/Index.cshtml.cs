using SAO.Asraes;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

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