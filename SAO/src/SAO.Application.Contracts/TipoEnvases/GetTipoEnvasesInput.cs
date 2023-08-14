using Volo.Abp.Application.Dtos;

namespace SAO.TipoEnvases
{
    public class GetTipoEnvasesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? DesEnvase { get; set; }

        public GetTipoEnvasesInput()
        {

        }
    }
}