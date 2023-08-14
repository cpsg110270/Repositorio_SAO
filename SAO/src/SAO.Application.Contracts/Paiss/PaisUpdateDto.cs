using System.ComponentModel.DataAnnotations;

namespace SAO.Paiss
{
    public class PaisUpdateDto
    {
        [Required]
        [StringLength(PaisConsts.NombrePaisMaxLength, MinimumLength = PaisConsts.NombrePaisMinLength)]
        public string NombrePais { get; set; }

    }
}