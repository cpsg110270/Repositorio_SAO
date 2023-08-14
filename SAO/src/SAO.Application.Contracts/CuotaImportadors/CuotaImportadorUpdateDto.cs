using System;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorUpdateDto
    {
        public int Año { get; set; }
        public decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }

    }
}