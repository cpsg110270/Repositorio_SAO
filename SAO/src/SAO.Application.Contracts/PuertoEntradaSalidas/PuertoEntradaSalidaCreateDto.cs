using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaCreateDto
    {
        [Required]
        [StringLength(PuertoEntradaSalidaConsts.NombrePuertoMaxLength, MinimumLength = PuertoEntradaSalidaConsts.NombrePuertoMinLength)]
        public string NombrePuerto { get; set; }
    }
}