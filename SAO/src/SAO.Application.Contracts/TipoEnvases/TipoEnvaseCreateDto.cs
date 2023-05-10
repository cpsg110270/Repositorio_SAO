using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseCreateDto
    {
        [Required]
        [StringLength(TipoEnvaseConsts.DesEnvaseMaxLength)]
        public string DesEnvase { get; set; }
    }
}