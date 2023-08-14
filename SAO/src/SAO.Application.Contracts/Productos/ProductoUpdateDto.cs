using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAO.Productos
{
    public class ProductoUpdateDto
    {
        [Required]
        [StringLength(ProductoConsts.NombreComerciaMaxLength, MinimumLength = ProductoConsts.NombreComerciaMinLength)]
        public string NombreComercia { get; set; }
        [StringLength(ProductoConsts.UsoMaxLength)]
        public string? Uso { get; set; }
        public Guid FabricanteId { get; set; }
        public int AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }
        public List<Guid> SustanciaElementalIds { get; set; }

    }
}