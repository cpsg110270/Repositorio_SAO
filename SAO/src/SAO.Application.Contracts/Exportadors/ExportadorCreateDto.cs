using System.ComponentModel.DataAnnotations;

namespace SAO.Exportadors
{
    public class ExportadorCreateDto
    {
        [Required]
        [StringLength(ExportadorConsts.NombreExportadorMaxLength, MinimumLength = ExportadorConsts.NombreExportadorMinLength)]
        public string NombreExportador { get; set; }
    }
}