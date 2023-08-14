using Volo.Abp.Application.Dtos;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaDto : EntityDto<int>
    {
        public string NombrePuerto { get; set; }

    }
}