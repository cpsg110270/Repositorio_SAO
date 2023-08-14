using Volo.Abp.Application.Dtos;

namespace SAO.Almacens
{
    public class GetAlmacensInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombreAlmacen { get; set; }
        public string? SiglaAlmacen { get; set; }

        public GetAlmacensInput()
        {

        }
    }
}