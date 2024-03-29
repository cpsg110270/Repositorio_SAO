using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Exportadors
{
    public class ExportadorCreateDto
    {
        public int NoImportador { get; set; }
        [Required]
        [StringLength(ExportadorConsts.NombreExportadorMaxLength, MinimumLength = ExportadorConsts.NombreExportadorMinLength)]
        public string NombreExportador { get; set; }
    }
}