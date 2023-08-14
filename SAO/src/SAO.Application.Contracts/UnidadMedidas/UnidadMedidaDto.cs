using Volo.Abp.Application.Dtos;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidaDto : EntityDto<int>
    {
        public string Abreviatura { get; set; }
        public string NombreUnidad { get; set; }

    }
}