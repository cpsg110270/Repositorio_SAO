using Volo.Abp.Application.Dtos;

namespace SAO.Fabricantes
{
    public class GetFabricantesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombreFabricante { get; set; }

        public GetFabricantesInput()
        {

        }
    }
}