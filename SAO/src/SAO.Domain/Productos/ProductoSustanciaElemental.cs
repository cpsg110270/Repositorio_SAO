using System;
using Volo.Abp.Domain.Entities;

namespace SAO.Productos
{
    public class ProductoSustanciaElemental : Entity
    {

        public Guid ProductoId { get; protected set; }

        public Guid SustanciaElementalId { get; protected set; }

        private ProductoSustanciaElemental()
        {

        }

        public ProductoSustanciaElemental(Guid productoId, Guid sustanciaElementalId)
        {
            ProductoId = productoId;
            SustanciaElementalId = sustanciaElementalId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    ProductoId,
                    SustanciaElementalId
                };
        }
    }
}