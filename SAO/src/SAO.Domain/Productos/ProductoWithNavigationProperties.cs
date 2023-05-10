using SAO.Fabricantes;
using SAO.Asraes;
using SAO.TipoProductos;
using SAO.SustanciaElementals;

using System;
using System.Collections.Generic;

namespace SAO.Productos
{
    public class ProductoWithNavigationProperties
    {
        public Producto Producto { get; set; }

        public Fabricante Fabricante { get; set; }
        public Asrae Asrae { get; set; }
        public TipoProducto TipoProducto { get; set; }
        

        public List<SustanciaElemental> SustanciaElementals { get; set; }
        
    }
}