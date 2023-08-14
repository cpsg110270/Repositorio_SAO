using System.ComponentModel.DataAnnotations;

namespace SAO.Paiss
{
    public class PaisCreateDto
    {
        [Required]
        [StringLength(PaisConsts.NombrePaisMaxLength, MinimumLength = PaisConsts.NombrePaisMinLength)]
        public string NombrePais { get; set; }
    }
}