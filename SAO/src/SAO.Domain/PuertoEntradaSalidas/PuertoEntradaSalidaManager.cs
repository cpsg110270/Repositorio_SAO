using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaManager : DomainService
    {
        private readonly IPuertoEntradaSalidaRepository _puertoEntradaSalidaRepository;

        public PuertoEntradaSalidaManager(IPuertoEntradaSalidaRepository puertoEntradaSalidaRepository)
        {
            _puertoEntradaSalidaRepository = puertoEntradaSalidaRepository;
        }

        public async Task<PuertoEntradaSalida> CreateAsync(
        string nombrePuerto)
        {
            Check.NotNullOrWhiteSpace(nombrePuerto, nameof(nombrePuerto));
            Check.Length(nombrePuerto, nameof(nombrePuerto), PuertoEntradaSalidaConsts.NombrePuertoMaxLength, PuertoEntradaSalidaConsts.NombrePuertoMinLength);

            var puertoEntradaSalida = new PuertoEntradaSalida(

             nombrePuerto
             );

            return await _puertoEntradaSalidaRepository.InsertAsync(puertoEntradaSalida);
        }

        public async Task<PuertoEntradaSalida> UpdateAsync(
            int id,
            string nombrePuerto
        )
        {
            Check.NotNullOrWhiteSpace(nombrePuerto, nameof(nombrePuerto));
            Check.Length(nombrePuerto, nameof(nombrePuerto), PuertoEntradaSalidaConsts.NombrePuertoMaxLength, PuertoEntradaSalidaConsts.NombrePuertoMinLength);

            var puertoEntradaSalida = await _puertoEntradaSalidaRepository.GetAsync(id);

            puertoEntradaSalida.NombrePuerto = nombrePuerto;

            return await _puertoEntradaSalidaRepository.UpdateAsync(puertoEntradaSalida);
        }

    }
}