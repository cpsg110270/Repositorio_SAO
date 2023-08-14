using SAO.Asraes;
using SAO.Fabricantes;
using SAO.SustanciaElementals;
using SAO.TipoProductos;
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