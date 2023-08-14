using System.ComponentModel.DataAnnotations;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaUpdateDto
    {
        [Required]
        [StringLength(PuertoEntradaSalidaConsts.NombrePuertoMaxLength, MinimumLength = PuertoEntradaSalidaConsts.NombrePuertoMinLength)]
        public string NombrePuerto { get; set; }

    }
}