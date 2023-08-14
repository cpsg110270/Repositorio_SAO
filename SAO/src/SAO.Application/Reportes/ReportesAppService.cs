using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SAO.Reportes
{
    public class ReportesAppService : ApplicationService, IReportesAppService
    {
        private readonly IReportesRepository _reporteRepository;
        public ReportesAppService(IReportesRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }
        public async Task<List<RepCuotasImportadoresDto>> GetCuotasImportadoresData(int? anio)
        {
            return ObjectMapper.Map<List<RepCuotasImportadores>, List<RepCuotasImportadoresDto>>(await _reporteRepository.GetCuotasImportadoresData(anio));
        }

        public async Task<List<RepPesosNetosASHRAEDto>> GetPesosNetosASHRAE(int? anio)
        {
            return ObjectMapper.Map<List<RepPesosNetosASHRAE>, List<RepPesosNetosASHRAEDto>>(await _reporteRepository.GetPesosNetosASHRAE(anio));
        }
    }
}
