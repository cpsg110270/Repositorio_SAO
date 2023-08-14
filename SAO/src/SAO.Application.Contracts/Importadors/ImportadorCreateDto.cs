using System.ComponentModel.DataAnnotations;

namespace SAO.Importadors
{
    public class ImportadorCreateDto
    {
        [Required]
        [StringLength(ImportadorConsts.NombreImportadorMaxLength, MinimumLength = ImportadorConsts.NombreImportadorMinLength)]
        public string NombreImportador { get; set; }
    }
}