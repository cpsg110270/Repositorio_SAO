using Volo.Abp.Modularity;

namespace SAO;

[DependsOn(
    typeof(SAOApplicationModule),
    typeof(SAODomainTestModule)
    )]
public class SAOApplicationTestModule : AbpModule
{

}
