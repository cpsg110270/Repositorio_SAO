using Volo.Abp.Application.Dtos;

namespace SAO.UnidadMedidas
{
    public class GetUnidadMedidasInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Abreviatura { get; set; }
        public string? NombreUnidad { get; set; }

        public GetUnidadMedidasInput()
        {

        }
    }
}