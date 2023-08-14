using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.Exportadors
{
    public class ExportadorManager : DomainService
    {
        private readonly IExportadorRepository _exportadorRepository;

        public ExportadorManager(IExportadorRepository exportadorRepository)
        {
            _exportadorRepository = exportadorRepository;
        }

        public async Task<Exportador> CreateAsync(
        string nombreExportador)
        {
            Check.NotNullOrWhiteSpace(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);

            var exportador = new Exportador(
             GuidGenerator.Create(),
             nombreExportador
             );

            return await _exportadorRepository.InsertAsync(exportador);
        }

        public async Task<Exportador> UpdateAsync(
            Guid id,
            string nombreExportador
        )
        {
            Check.NotNullOrWhiteSpace(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);

            var exportador = await _exportadorRepository.GetAsync(id);

            exportador.NombreExportador = nombreExportador;

            return await _exportadorRepository.UpdateAsync(exportador);
        }

    }
}