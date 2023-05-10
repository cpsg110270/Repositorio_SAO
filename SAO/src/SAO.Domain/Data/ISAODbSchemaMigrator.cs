using System.Threading.Tasks;

namespace SAO.Data;

public interface ISAODbSchemaMigrator
{
    Task MigrateAsync();
}
