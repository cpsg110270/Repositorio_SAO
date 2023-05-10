using SAO.Fabricantes;
using SAO.Asraes;
using SAO.TipoProductos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.Productos
{
    public class Producto : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string NombreComercia { get; set; }

        [CanBeNull]
        public virtual string? Uso { get; set; }
        public Guid FabricanteId { get; set; }
        public int AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }
        public ICollection<ProductoSustanciaElemental> SustanciaElementals { get; private set; }

        public Producto()
        {

        }

        public Producto(Guid id, Guid fabricanteId, int asraeId, Guid? tipoProductoId, string nombreComercia, string uso)
        {

            Id = id;
            Check.NotNull(nombreComercia, nameof(nombreComercia));
            Check.Length(nombreComercia, nameof(nombreComercia), ProductoConsts.NombreComerciaMaxLength, ProductoConsts.NombreComerciaMinLength);
            Check.Length(uso, nameof(uso), ProductoConsts.UsoMaxLength, 0);
            NombreComercia = nombreComercia;
            Uso = uso;
            FabricanteId = fabricanteId;
            AsraeId = asraeId;
            TipoProductoId = tipoProductoId;
            SustanciaElementals = new Collection<ProductoSustanciaElemental>();
        }
        public void AddSustanciaElemental(Guid sustanciaElementalId)
        {
            Check.NotNull(sustanciaElementalId, nameof(sustanciaElementalId));

            if (IsInSustanciaElementals(sustanciaElementalId))
            {
                return;
            }

            SustanciaElementals.Add(new ProductoSustanciaElemental(Id, sustanciaElementalId));
        }

        public void RemoveSustanciaElemental(Guid sustanciaElementalId)
        {
            Check.NotNull(sustanciaElementalId, nameof(sustanciaElementalId));

            if (!IsInSustanciaElementals(sustanciaElementalId))
            {
                return;
            }

            SustanciaElementals.RemoveAll(x => x.SustanciaElementalId == sustanciaElementalId);
        }

        public void RemoveAllSustanciaElementalsExceptGivenIds(List<Guid> sustanciaElementalIds)
        {
            Check.NotNullOrEmpty(sustanciaElementalIds, nameof(sustanciaElementalIds));

            SustanciaElementals.RemoveAll(x => !sustanciaElementalIds.Contains(x.SustanciaElementalId));
        }

        public void RemoveAllSustanciaElementals()
        {
            SustanciaElementals.RemoveAll(x => x.ProductoId == Id);
        }

        private bool IsInSustanciaElementals(Guid sustanciaElementalId)
        {
            return SustanciaElementals.Any(x => x.SustanciaElementalId == sustanciaElementalId);
        }
    }
}