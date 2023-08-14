using System.ComponentModel.DataAnnotations;

namespace SAO.TipoPermisos
{
    public class TipoPermisoCreateDto
    {
        [Required]
        [StringLength(TipoPermisoConsts.CodigoMaxLength, MinimumLength = TipoPermisoConsts.CodigoMinLength)]
        public string Codigo { get; set; }
        [StringLength(TipoPermisoConsts.DesripcionMaxLength)]
        public string? Desripcion { get; set; }
    }
}