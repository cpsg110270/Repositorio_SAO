using System.ComponentModel.DataAnnotations;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseUpdateDto
    {
        [Required]
        [StringLength(TipoEnvaseConsts.DesEnvaseMaxLength)]
        public string DesEnvase { get; set; }

    }
}