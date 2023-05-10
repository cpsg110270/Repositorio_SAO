using System;
using Volo.Abp.Application.Dtos;

namespace SAO.TipoProductos
{
    public class TipoProductoDto : EntityDto<Guid>
    {
        public string DesProducto { get; set; }

    }
}