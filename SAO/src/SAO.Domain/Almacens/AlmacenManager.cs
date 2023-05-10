using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.Almacens
{
    public class AlmacenManager : DomainService
    {
        private readonly IAlmacenRepository _almacenRepository;

        public AlmacenManager(IAlmacenRepository almacenRepository)
        {
            _almacenRepository = almacenRepository;
        }

        public async Task<Almacen> CreateAsync(
        string nombreAlmacen, string siglaAlmacen)
        {
            Check.NotNullOrWhiteSpace(nombreAlmacen, nameof(nombreAlmacen));
            Check.Length(nombreAlmacen, nameof(nombreAlmacen), AlmacenConsts.NombreAlmacenMaxLength);
            Check.Length(siglaAlmacen, nameof(siglaAlmacen), AlmacenConsts.SiglaAlmacenMaxLength);

            var almacen = new Almacen(

             nombreAlmacen, siglaAlmacen
             );

            return await _almacenRepository.InsertAsync(almacen);
        }

        public async Task<Almacen> UpdateAsync(
            int id,
            string nombreAlmacen, string siglaAlmacen
        )
        {
            Check.NotNullOrWhiteSpace(nombreAlmacen, nameof(nombreAlmacen));
            Check.Length(nombreAlmacen, nameof(nombreAlmacen), AlmacenConsts.NombreAlmacenMaxLength);
            Check.Length(siglaAlmacen, nameof(siglaAlmacen), AlmacenConsts.SiglaAlmacenMaxLength);

            var almacen = await _almacenRepository.GetAsync(id);

            almacen.NombreAlmacen = nombreAlmacen;
            almacen.SiglaAlmacen = siglaAlmacen;

            return await _almacenRepository.UpdateAsync(almacen);
        }

    }
}