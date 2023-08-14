using Volo.Abp.Application.Dtos;

namespace SAO.Exportadors
{
    public class GetExportadorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombreExportador { get; set; }

        public GetExportadorsInput()
        {

        }
    }
}