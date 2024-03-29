using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Importadors
{
    public class ImportadorUpdateDto
    {
        public int NoImportador { get; set; }
        [StringLength(ImportadorConsts.NoRUCMaxLength)]
        public string? NoRUC { get; set; }
        [Required]
        [StringLength(ImportadorConsts.NombreImportadorMaxLength, MinimumLength = ImportadorConsts.NombreImportadorMinLength)]
        public string NombreImportador { get; set; }

    }
}