using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Fabricantes
{
    public class FabricanteCreateDto
    {
        [Required]
        [StringLength(FabricanteConsts.NombreFabricanteMaxLength, MinimumLength = FabricanteConsts.NombreFabricanteMinLength)]
        public string NombreFabricante { get; set; }
    }
}