using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorManager : DomainService
    {
        private readonly ICuotaImportadorRepository _cuotaImportadorRepository;

        public CuotaImportadorManager(ICuotaImportadorRepository cuotaImportadorRepository)
        {
            _cuotaImportadorRepository = cuotaImportadorRepository;
        }

        public async Task<CuotaImportador> CreateAsync(
        Guid importadorId, int? asraeId, Guid? tipoProductoId, int año, decimal cuota)
        {
            Check.NotNull(importadorId, nameof(importadorId));

            var cuotaImportador = new CuotaImportador(
             GuidGenerator.Create(),
             importadorId, asraeId, tipoProductoId, año, cuota
             );

            return await _cuotaImportadorRepository.InsertAsync(cuotaImportador);
        }

        public async Task<CuotaImportador> UpdateAsync(
            Guid id,
            Guid importadorId, int? asraeId, Guid? tipoProductoId, int año, decimal cuota
        )
        {
            Check.NotNull(importadorId, nameof(importadorId));

            var cuotaImportador = await _cuotaImportadorRepository.GetAsync(id);

            cuotaImportador.ImportadorId = importadorId;
            cuotaImportador.AsraeId = asraeId;
            cuotaImportador.TipoProductoId = tipoProductoId;
            cuotaImportador.Año = año;
            cuotaImportador.Cuota = cuota;

            return await _cuotaImportadorRepository.UpdateAsync(cuotaImportador);
        }

    }
}