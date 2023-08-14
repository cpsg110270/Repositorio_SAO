using System.ComponentModel.DataAnnotations;

namespace SAO.Asraes
{
    public class AsraeCreateDto
    {
        [Required]
        [StringLength(AsraeConsts.Codigo_ASHRAEMaxLength, MinimumLength = AsraeConsts.Codigo_ASHRAEMinLength)]
        public string Codigo_ASHRAE { get; set; }
        public string? Descripcion { get; set; }
    }
}