using Volo.Abp.Application.Dtos;

namespace SAO.PuertoEntradaSalidas
{
    public class GetPuertoEntradaSalidasInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombrePuerto { get; set; }

        public GetPuertoEntradaSalidasInput()
        {

        }
    }
}