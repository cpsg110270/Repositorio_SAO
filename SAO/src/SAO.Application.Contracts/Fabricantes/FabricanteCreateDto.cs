using System.ComponentModel.DataAnnotations;

namespace SAO.Fabricantes
{
    public class FabricanteCreateDto
    {
        [Required]
        [StringLength(FabricanteConsts.NombreFabricanteMaxLength, MinimumLength = FabricanteConsts.NombreFabricanteMinLength)]
        public string NombreFabricante { get; set; }
    }
}