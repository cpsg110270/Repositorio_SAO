using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.TipoProductos
{
    public class TipoProducto : Entity<Guid>
    {
        [NotNull]
        public virtual string DesProducto { get; set; }

        public TipoProducto()
        {

        }

        public TipoProducto(Guid id, string desProducto)
        {

            Id = id;
            Check.NotNull(desProducto, nameof(desProducto));
            Check.Length(desProducto, nameof(desProducto), TipoProductoConsts.DesProductoMaxLength, TipoProductoConsts.DesProductoMinLength);
            DesProducto = desProducto;
        }

    }
}