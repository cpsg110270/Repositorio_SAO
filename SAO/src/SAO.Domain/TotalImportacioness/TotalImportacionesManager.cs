using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesManager : DomainService
    {
        private readonly ITotalImportacionesRepository _totalImportacionesRepository;

        public TotalImportacionesManager(ITotalImportacionesRepository totalImportacionesRepository)
        {
            _totalImportacionesRepository = totalImportacionesRepository;
        }

        public async Task<TotalImportaciones> CreateAsync(
        Guid importadorId, Guid tipoProductoId, int asraeId, int anio, double cuotaAsignada, double? cuotaConsumida = null)
        {
            Check.NotNull(importadorId, nameof(importadorId));
            Check.NotNull(tipoProductoId, nameof(tipoProductoId));
            Check.NotNull(asraeId, nameof(asraeId));

            var totalImportaciones = new TotalImportaciones(
             GuidGenerator.Create(),
             importadorId, tipoProductoId, asraeId, anio, cuotaAsignada, cuotaConsumida
             );

            return await _totalImportacionesRepository.InsertAsync(totalImportaciones);
        }

        public async Task<TotalImportaciones> UpdateAsync(
            Guid id,
            Guid importadorId, Guid tipoProductoId, int asraeId, int anio, double cuotaAsignada, double? cuotaConsumida = null
        )
        {
            Check.NotNull(importadorId, nameof(importadorId));
            Check.NotNull(tipoProductoId, nameof(tipoProductoId));
            Check.NotNull(asraeId, nameof(asraeId));

            var totalImportaciones = await _totalImportacionesRepository.GetAsync(id);

            totalImportaciones.ImportadorId = importadorId;
            totalImportaciones.TipoProductoId = tipoProductoId;
            totalImportaciones.AsraeId = asraeId;
            totalImportaciones.Anio = anio;
            totalImportaciones.CuotaAsignada = cuotaAsignada;
            totalImportaciones.CuotaConsumida = cuotaConsumida;

            return await _totalImportacionesRepository.UpdateAsync(totalImportaciones);
        }

    }
}