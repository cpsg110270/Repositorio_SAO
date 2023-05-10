using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Importadors
{
    public class ImportadorCreateDto
    {
        [Required]
        [StringLength(ImportadorConsts.NombreImportadorMaxLength, MinimumLength = ImportadorConsts.NombreImportadorMinLength)]
        public string NombreImportador { get; set; }
    }
}