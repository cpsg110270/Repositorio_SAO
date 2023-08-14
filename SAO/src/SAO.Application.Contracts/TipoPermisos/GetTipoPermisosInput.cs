using Volo.Abp.Application.Dtos;

namespace SAO.TipoPermisos
{
    public class GetTipoPermisosInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Codigo { get; set; }
        public string? Desripcion { get; set; }

        public GetTipoPermisosInput()
        {

        }
    }
}