using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.Paiss
{
    public class PaisManager : DomainService
    {
        private readonly IPaisRepository _paisRepository;

        public PaisManager(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }

        public async Task<Pais> CreateAsync(
        string nombrePais)
        {
            Check.NotNullOrWhiteSpace(nombrePais, nameof(nombrePais));
            Check.Length(nombrePais, nameof(nombrePais), PaisConsts.NombrePaisMaxLength, PaisConsts.NombrePaisMinLength);

            var pais = new Pais(

             nombrePais
             );

            return await _paisRepository.InsertAsync(pais);
        }

        public async Task<Pais> UpdateAsync(
            int id,
            string nombrePais
        )
        {
            Check.NotNullOrWhiteSpace(nombrePais, nameof(nombrePais));
            Check.Length(nombrePais, nameof(nombrePais), PaisConsts.NombrePaisMaxLength, PaisConsts.NombrePaisMinLength);

            var pais = await _paisRepository.GetAsync(id);

            pais.NombrePais = nombrePais;

            return await _paisRepository.UpdateAsync(pais);
        }

    }
}