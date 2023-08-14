using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.Exportadors
{
    public class Exportador : Entity<Guid>
    {
        [NotNull]
        public virtual string NombreExportador { get; set; }

        public Exportador()
        {

        }

        public Exportador(Guid id, string nombreExportador)
        {

            Id = id;
            Check.NotNull(nombreExportador, nameof(nombreExportador));
            Check.Length(nombreExportador, nameof(nombreExportador), ExportadorConsts.NombreExportadorMaxLength, ExportadorConsts.NombreExportadorMinLength);
            NombreExportador = nombreExportador;
        }

    }
}