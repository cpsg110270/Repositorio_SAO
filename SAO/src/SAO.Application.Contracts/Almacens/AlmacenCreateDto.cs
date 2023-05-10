using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.Almacens
{
    public class AlmacenCreateDto
    {
        [Required]
        [StringLength(AlmacenConsts.NombreAlmacenMaxLength)]
        public string NombreAlmacen { get; set; }
        [StringLength(AlmacenConsts.SiglaAlmacenMaxLength)]
        public string? SiglaAlmacen { get; set; }
    }
}