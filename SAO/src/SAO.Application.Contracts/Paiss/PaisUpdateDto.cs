using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Paiss
{
    public class PaisUpdateDto
    {
        [Required]
        [StringLength(PaisConsts.NombrePaisMaxLength, MinimumLength = PaisConsts.NombrePaisMinLength)]
        public string NombrePais { get; set; }

    }
}