using SAO.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SAO;

[DependsOn(
    typeof(SAOEntityFrameworkCoreTestModule)
    )]
public class SAODomainTestModule : AbpModule
{

}
