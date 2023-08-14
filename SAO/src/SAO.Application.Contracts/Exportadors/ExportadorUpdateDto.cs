using System.ComponentModel.DataAnnotations;

namespace SAO.Exportadors
{
    public class ExportadorUpdateDto
    {
        [Required]
        [StringLength(ExportadorConsts.NombreExportadorMaxLength, MinimumLength = ExportadorConsts.NombreExportadorMinLength)]
        public string NombreExportador { get; set; }

    }
}