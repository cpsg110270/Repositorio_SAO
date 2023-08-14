using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAO.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SAO.EntityFrameworkCore;

public class EntityFrameworkCoreSAODbSchemaMigrator
    : ISAODbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSAODbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SAODbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SAODbContext>()
            .Database
            .MigrateAsync();
    }
}
