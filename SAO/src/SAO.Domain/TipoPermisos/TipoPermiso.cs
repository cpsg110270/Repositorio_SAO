using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SAO.TipoPermisos
{
    public class TipoPermiso : Entity<Guid>
    {
        [NotNull]
        public virtual string Codigo { get; set; }

        [CanBeNull]
        public virtual string? Desripcion { get; set; }

        public TipoPermiso()
        {

        }

        public TipoPermiso(Guid id, string codigo, string desripcion)
        {

            Id = id;
            Check.NotNull(codigo, nameof(codigo));
            Check.Length(codigo, nameof(codigo), TipoPermisoConsts.CodigoMaxLength, TipoPermisoConsts.CodigoMinLength);
            Check.Length(desripcion, nameof(desripcion), TipoPermisoConsts.DesripcionMaxLength, 0);
            Codigo = codigo;
            Desripcion = desripcion;
        }

    }
}