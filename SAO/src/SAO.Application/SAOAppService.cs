using SAO.Localization;
using Volo.Abp.Application.Services;

namespace SAO;

/* Inherit your application services from this class.
 */
public abstract class SAOAppService : ApplicationService
{
    protected SAOAppService()
    {
        LocalizationResource = typeof(SAOResource);
    }
}
