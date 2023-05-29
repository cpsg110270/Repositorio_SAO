using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorCreateDto
    {
        public int Año { get; set; }
        public decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }
    }
}