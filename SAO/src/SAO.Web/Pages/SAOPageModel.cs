using SAO.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SAO.Web.Pages;

public abstract class SAOPageModel : AbpPageModel
{
    protected SAOPageModel()
    {
        LocalizationResourceType = typeof(SAOResource);
    }
}
