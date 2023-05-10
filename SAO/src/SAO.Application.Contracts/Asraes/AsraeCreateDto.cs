using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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