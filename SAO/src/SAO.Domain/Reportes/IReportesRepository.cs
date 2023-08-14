using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Reportes
{
    public interface IReportesRepository : IRepository
    {
        Task<List<RepCuotasImportadores>> GetCuotasImportadoresData(int? anio);
        Task<List<RepPesosNetosASHRAE>> GetPesosNetosASHRAE(int? anio);
    }
}
