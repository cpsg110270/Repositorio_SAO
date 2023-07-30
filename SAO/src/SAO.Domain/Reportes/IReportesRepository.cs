using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.Reportes
{
    public interface IReportesRepository : IRepository
    {
        Task<List<RepCuotasImportadores>> GetCuotasImportadoresData();
        Task<List<RepPesosNetosASHRAE>> GetPesosNetosASHRAE();
    }
}
