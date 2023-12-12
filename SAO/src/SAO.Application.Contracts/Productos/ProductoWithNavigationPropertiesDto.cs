using SAO.Fabricantes;
using SAO.Asraes;
using SAO.TipoProductos;
using SAO.SustanciaElementals;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace SAO.Productos
{
    public class ProductoWithNavigationPropertiesDto
    {
        public ProductoDto Producto { get; set; }

        public FabricanteDto Fabricante { get; set; }
        public AsraeDto Asrae { get; set; }
        public TipoProductoDto TipoProducto { get; set; }
        public List<SustanciaElementalDto> SustanciaElementals { get; set; }

    }
}