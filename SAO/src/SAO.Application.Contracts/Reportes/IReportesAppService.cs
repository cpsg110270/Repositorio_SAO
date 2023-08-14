using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SAO.Reportes
{
    public interface IReportesAppService : IApplicationService
    {
        Task<List<RepCuotasImportadoresDto>> GetCuotasImportadoresData(int? anio);
        Task<List<RepPesosNetosASHRAEDto>> GetPesosNetosASHRAE(int? anio);
    }
}
