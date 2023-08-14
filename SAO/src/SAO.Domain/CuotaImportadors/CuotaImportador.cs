using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SAO.CuotaImportadors
{
    public class CuotaImportador : AuditedEntity<Guid>
    {
        public virtual int Año { get; set; }

        public virtual decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }

        public CuotaImportador()
        {

        }

        public CuotaImportador(Guid id, Guid importadorId, int año, decimal cuota)
        {

            Id = id;
            Año = año;
            Cuota = cuota;
            ImportadorId = importadorId;
        }

    }
}