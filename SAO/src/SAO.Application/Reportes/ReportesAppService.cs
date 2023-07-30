using AutoMapper.Internal.Mappers;
using Org.BouncyCastle.Utilities;
using SAO.CuotaImportadors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace SAO.Reportes
{
    public class ReportesAppService : ApplicationService, IReportesAppService
    {
        private readonly IReportesRepository _reporteRepository;
        public ReportesAppService(IReportesRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }
        public async Task<List<RepCuotasImportadoresDto>> GetCuotasImportadoresData()
        {
            return ObjectMapper.Map<List<RepCuotasImportadores>, List<RepCuotasImportadoresDto>>(await _reporteRepository.GetCuotasImportadoresData());
        }

        public async Task<List<RepPesosNetosASHRAEDto>> GetPesosNetosASHRAE()
        {
            return ObjectMapper.Map<List<RepPesosNetosASHRAE>, List<RepPesosNetosASHRAEDto>>(await _reporteRepository.GetPesosNetosASHRAE());
        }
    }
}
