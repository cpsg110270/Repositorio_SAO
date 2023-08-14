using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SAO.Web;

[Dependency(ReplaceServices = true)]
public class SAOBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SAO";
}
