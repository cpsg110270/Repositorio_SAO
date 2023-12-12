using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.Exportadors
{
    public class Exportador : Entity<Guid>
    {
        public virtual int NoImportador { get; set; }

        [NotNull]
        public virtual string NombreExportador { get; set; }

        public Exportador()
        {

        }

        public Exportador(Guid id, int noImportador, string nombreExportador)
        {

            Id = id;
            Check.NotNull(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);
            NoImportador = noImportador;
            NombreExportador = nombreExportador;
        }

    }
}