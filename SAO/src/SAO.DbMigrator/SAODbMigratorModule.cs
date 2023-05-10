using SAO.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SAO.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SAOEntityFrameworkCoreModule),
    typeof(SAOApplicationContractsModule)
)]
public class SAODbMigratorModule : AbpModule
{

}
