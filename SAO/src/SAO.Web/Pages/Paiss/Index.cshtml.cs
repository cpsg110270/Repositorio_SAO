using SAO.Paiss;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

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