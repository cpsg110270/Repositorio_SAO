using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        int noImportador, string nombreExportador)
        {
            Check.NotNullOrWhiteSpace(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);

            var exportador = new Exportador(
             GuidGenerator.Create(),
             noImportador, nombreExportador
             );

            return await _exportadorRepository.InsertAsync(exportador);
        }

        public async Task<Exportador> UpdateAsync(
            Guid id,
            int noImportador, string nombreExportador
        )
        {
            Check.NotNullOrWhiteSpace(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);

            var exportador = await _exportadorRepository.GetAsync(id);

            exportador.NoImportador = noImportador;
            exportador.NombreExportador = nombreExportador;

            return await _exportadorRepository.UpdateAsync(exportador);
        }

    }
}