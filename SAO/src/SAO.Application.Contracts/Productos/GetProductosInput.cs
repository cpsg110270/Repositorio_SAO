using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Productos
{
    public class GetProductosInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? NoProductoMin { get; set; }
        public int? NoProductoMax { get; set; }
        public string? NombreComercia { get; set; }
        public string? Uso { get; set; }
        public Guid? FabricanteId { get; set; }
        public int? AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }
        public Guid? SustanciaElementalId { get; set; }

        public GetProductosInput()
        {

        }
    }
}