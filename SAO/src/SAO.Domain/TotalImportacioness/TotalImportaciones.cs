using SAO.Importadors;
using SAO.TipoProductos;
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

namespace SAO.TotalImportacioness
{
    public class TotalImportaciones : FullAuditedEntity<Guid>
    {
        public virtual int Anio { get; set; }

        public virtual double CuotaAsignada { get; set; }

        public virtual double? CuotaConsumida { get; set; }
        public Guid ImportadorId { get; set; }
        public Guid TipoProductoId { get; set; }
        public int AsraeId { get; set; }

        public TotalImportaciones()
        {

        }

        public TotalImportaciones(Guid id, Guid importadorId, Guid tipoProductoId, int asraeId, int anio, double cuotaAsignada, double? cuotaConsumida = null)
        {

            Id = id;
            Anio = anio;
            CuotaAsignada = cuotaAsignada;
            CuotaConsumida = cuotaConsumida;
            ImportadorId = importadorId;
            TipoProductoId = tipoProductoId;
            AsraeId = asraeId;
        }

    }
}