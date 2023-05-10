using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SAO.Data;

/* This is used if database provider does't define
 * ISAODbSchemaMigrator implementation.
 */
public class NullSAODbSchemaMigrator : ISAODbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
