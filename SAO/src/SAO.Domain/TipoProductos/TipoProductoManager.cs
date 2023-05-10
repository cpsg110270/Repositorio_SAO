using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SAO.TipoProductos
{
    public class TipoProductoManager : DomainService
    {
        private readonly ITipoProductoRepository _tipoProductoRepository;

        public TipoProductoManager(ITipoProductoRepository tipoProductoRepository)
        {
            _tipoProductoRepository = tipoProductoRepository;
        }

        public async Task<TipoProducto> CreateAsync(
        string desProducto)
        {
            Check.NotNullOrWhiteSpace(desProducto, nameof(desProducto));
            Check.Length(desProducto, nameof(desProducto), TipoProductoConsts.DesProductoMaxLength, TipoProductoConsts.DesProductoMinLength);

            var tipoProducto = new TipoProducto(
             GuidGenerator.Create(),
             desProducto
             );

            return await _tipoProductoRepository.InsertAsync(tipoProducto);
        }

        public async Task<TipoProducto> UpdateAsync(
            Guid id,
            string desProducto
        )
        {
            Check.NotNullOrWhiteSpace(desProducto, nameof(desProducto));
            Check.Length(desProducto, nameof(desProducto), TipoProductoConsts.DesProductoMaxLength, TipoProductoConsts.DesProductoMinLength);

            var tipoProducto = await _tipoProductoRepository.GetAsync(id);

            tipoProducto.DesProducto = desProducto;

            return await _tipoProductoRepository.UpdateAsync(tipoProducto);
        }

    }
}