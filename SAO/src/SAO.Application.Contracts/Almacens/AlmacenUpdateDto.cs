using System.ComponentModel.DataAnnotations;

namespace SAO.Almacens
{
    public class AlmacenUpdateDto
    {
        [Required]
        [StringLength(AlmacenConsts.NombreAlmacenMaxLength)]
        public string NombreAlmacen { get; set; }
        [StringLength(AlmacenConsts.SiglaAlmacenMaxLength)]
        public string? SiglaAlmacen { get; set; }

    }
}