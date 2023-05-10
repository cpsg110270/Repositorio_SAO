using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

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