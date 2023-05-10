using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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