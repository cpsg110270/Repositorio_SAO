using System.ComponentModel.DataAnnotations;

namespace SAO.TipoProductos
{
    public class TipoProductoUpdateDto
    {
        [Required]
        [StringLength(TipoProductoConsts.DesProductoMaxLength, MinimumLength = TipoProductoConsts.DesProductoMinLength)]
        public string DesProducto { get; set; }

    }
}