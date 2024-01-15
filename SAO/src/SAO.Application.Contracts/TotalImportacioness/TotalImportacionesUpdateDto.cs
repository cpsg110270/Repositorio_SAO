using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesUpdateDto
    {
        public int Anio { get; set; }
        public double CuotaAsignada { get; set; }
        public double? CuotaConsumida { get; set; }
        public Guid ImportadorId { get; set; }
        public Guid TipoProductoId { get; set; }
        public int AsraeId { get; set; }

    }
}