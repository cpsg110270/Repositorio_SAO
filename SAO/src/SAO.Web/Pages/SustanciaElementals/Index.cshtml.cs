using SAO.SustanciaElementals;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

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