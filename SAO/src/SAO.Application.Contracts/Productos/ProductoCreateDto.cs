using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Productos
{
    public class ProductoCreateDto
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