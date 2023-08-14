using SAO.Almacens;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

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