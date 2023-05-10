using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.Fabricantes
{
    public class FabricanteManager : DomainService
    {
        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteManager(IFabricanteRepository fabricanteRepository)
        {
            _fabricanteRepository = fabricanteRepository;
        }

        public async Task<Fabricante> CreateAsync(
        string nombreFabricante)
        {
            Check.NotNullOrWhiteSpace(nombreFabricante, nameof(nombreFabricante));
            Check.Length(nombreFabricante, nameof(nombreFabricante), FabricanteConsts.NombreFabricanteMaxLength, FabricanteConsts.NombreFabricanteMinLength);

            var fabricante = new Fabricante(
             GuidGenerator.Create(),
             nombreFabricante
             );

            return await _fabricanteRepository.InsertAsync(fabricante);
        }

        public async Task<Fabricante> UpdateAsync(
            Guid id,
            string nombreFabricante
        )
        {
            Check.NotNullOrWhiteSpace(nombreFabricante, nameof(nombreFabricante));
            Check.Length(nombreFabricante, nameof(nombreFabricante), FabricanteConsts.NombreFabricanteMaxLength, FabricanteConsts.NombreFabricanteMinLength);

            var fabricante = await _fabricanteRepository.GetAsync(id);

            fabricante.NombreFabricante = nombreFabricante;

            return await _fabricanteRepository.UpdateAsync(fabricante);
        }

    }
}