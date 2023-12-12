using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Productos
{
    public class ProductoDto : FullAuditedEntityDto<Guid>
    {
        public int NoProducto { get; set; }
        public string NombreComercia { get; set; }
        public string? Uso { get; set; }
        public Guid FabricanteId { get; set; }
        public int AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }

    }
}