using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.Importadors
{
    public class ImportadorManager : DomainService
    {
        private readonly IImportadorRepository _importadorRepository;

        public ImportadorManager(IImportadorRepository importadorRepository)
        {
            _importadorRepository = importadorRepository;
        }

        public async Task<Importador> CreateAsync(
        string nombreImportador)
        {
            Check.NotNullOrWhiteSpace(nombreImportador, nameof(nombreImportador));
            Check.Length(nombreImportador, nameof(nombreImportador), ImportadorConsts.NombreImportadorMaxLength, ImportadorConsts.NombreImportadorMinLength);

            var importador = new Importador(
             GuidGenerator.Create(),
             nombreImportador
             );

            return await _importadorRepository.InsertAsync(importador);
        }

        public async Task<Importador> UpdateAsync(
            Guid id,
            string nombreImportador
        )
        {
            Check.NotNullOrWhiteSpace(nombreImportador, nameof(nombreImportador));
            Check.Length(nombreImportador, nameof(nombreImportador), ImportadorConsts.NombreImportadorMaxLength, ImportadorConsts.NombreImportadorMinLength);

            var importador = await _importadorRepository.GetAsync(id);

            importador.NombreImportador = nombreImportador;

            return await _importadorRepository.UpdateAsync(importador);
        }

    }
}