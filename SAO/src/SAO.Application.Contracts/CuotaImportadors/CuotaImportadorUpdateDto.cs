using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorUpdateDto
    {
        public int Año { get; set; }
        public decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }
        public int? AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }

    }
}