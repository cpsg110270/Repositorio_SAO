using SAO.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SAO.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SAOController : AbpControllerBase
{
    protected SAOController()
    {
        LocalizationResource = typeof(SAOResource);
    }
}
