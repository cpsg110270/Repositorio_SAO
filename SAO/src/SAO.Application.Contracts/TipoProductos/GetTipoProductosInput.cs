using Volo.Abp.Application.Dtos;

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