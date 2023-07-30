using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SAO.Reportes
{
    public interface IReportesAppService : IApplicationService
    {
        Task<List<RepCuotasImportadoresDto>> GetCuotasImportadoresData();
        Task<List<RepPesosNetosASHRAEDto>> GetPesosNetosASHRAE();
    }
}
