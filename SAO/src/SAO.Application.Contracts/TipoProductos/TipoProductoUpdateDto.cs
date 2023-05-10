using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.TipoProductos
{
    public class TipoProductoUpdateDto
    {
        [Required]
        [StringLength(TipoProductoConsts.DesProductoMaxLength, MinimumLength = TipoProductoConsts.DesProductoMinLength)]
        public string DesProducto { get; set; }

    }
}