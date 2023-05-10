using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalCreateDto
    {
        [Required]
        [StringLength(SustanciaElementalConsts.CodCasMaxLength, MinimumLength = SustanciaElementalConsts.CodCasMinLength)]
        public string CodCas { get; set; }
        [Required]
        [StringLength(SustanciaElementalConsts.DesSustanciaMaxLength, MinimumLength = SustanciaElementalConsts.DesSustanciaMinLength)]
        public string DesSustancia { get; set; }
    }
}