using System.ComponentModel.DataAnnotations;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseCreateDto
    {
        [Required]
        [StringLength(TipoEnvaseConsts.DesEnvaseMaxLength)]
        public string DesEnvase { get; set; }
    }
}