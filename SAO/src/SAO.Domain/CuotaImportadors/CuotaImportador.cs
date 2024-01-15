using SAO.Importadors;
using SAO.Asraes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.CuotaImportadors
{
    public class CuotaImportador : AuditedEntity<Guid>
    {
        public virtual int Año { get; set; }

        public virtual decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }
        public int? AsraeId { get; set; }

        public CuotaImportador()
        {

        }

        public CuotaImportador(Guid id, Guid importadorId, int? asraeId, int año, decimal cuota)
        {

            Id = id;
            Año = año;
            Cuota = cuota;
            ImportadorId = importadorId;
            AsraeId = asraeId;
        }

    }
}