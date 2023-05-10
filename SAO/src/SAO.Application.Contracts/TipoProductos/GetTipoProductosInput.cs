using Volo.Abp.Application.Dtos;
using System;

namespace SAO.TipoProductos
{
    public class GetTipoProductosInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? DesProducto { get; set; }

        public GetTipoProductosInput()
        {

        }
    }
}