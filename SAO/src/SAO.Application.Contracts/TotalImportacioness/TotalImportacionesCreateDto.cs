using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesCreateDto
    {
        public int Anio { get; set; }
        public double CuotaAsignada { get; set; } = 0;
        public double? CuotaConsumida { get; set; } = 0;
        public Guid ImportadorId { get; set; }
        public Guid TipoProductoId { get; set; }
        public int AsraeId { get; set; }
    }
}