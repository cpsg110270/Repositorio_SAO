using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace SAO.Web;

[Dependency(ReplaceServices = true)]
public class SAOBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SAO";
}
