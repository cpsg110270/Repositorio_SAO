using System.ComponentModel.DataAnnotations;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidaCreateDto
    {
        [Required]
        [StringLength(UnidadMedidaConsts.AbreviaturaMaxLength, MinimumLength = UnidadMedidaConsts.AbreviaturaMinLength)]
        public string Abreviatura { get; set; }
        [Required]
        [StringLength(UnidadMedidaConsts.NombreUnidadMaxLength)]
        public string NombreUnidad { get; set; }
    }
}