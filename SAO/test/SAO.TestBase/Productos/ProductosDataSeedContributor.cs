using SAO.TipoProductos;
using SAO.Asraes;
using SAO.Fabricantes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.Productos;

namespace SAO.Productos
{
    public class ProductosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProductoRepository _productoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly FabricantesDataSeedContributor _fabricantesDataSeedContributor;

        private readonly AsraesDataSeedContributor _asraesDataSeedContributor;

        private readonly TipoProductosDataSeedContributor _tipoProductosDataSeedContributor;

        public ProductosDataSeedContributor(IProductoRepository productoRepository, IUnitOfWorkManager unitOfWorkManager, FabricantesDataSeedContributor fabricantesDataSeedContributor, AsraesDataSeedContributor asraesDataSeedContributor, TipoProductosDataSeedContributor tipoProductosDataSeedContributor)
        {
            _productoRepository = productoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _fabricantesDataSeedContributor = fabricantesDataSeedContributor; _asraesDataSeedContributor = asraesDataSeedContributor; _tipoProductosDataSeedContributor = tipoProductosDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _fabricantesDataSeedContributor.SeedAsync(context);
            await _asraesDataSeedContributor.SeedAsync(context);
            await _tipoProductosDataSeedContributor.SeedAsync(context);

            await _productoRepository.InsertAsync(new Producto
            (
                id: Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"),
                noProducto: 204154099,
                nombreComercia: "ac34139e76984a0296adbdb08a191f64c4d636e162564ec6a4d69293d72b5d4b0e807c81d4fd4d2f84645b246b",
                uso: "cec430b57fd949288199bffd43954a63f31c2a4e6d504c0aa488e098ad0d58c44ba7d8965435426394b9ff2de05bb6d4bd0519c4827e405b87b1c167e2b2aad3b310beeb38034863940dbea79edaa8716ab43221deaf44ef9d905973de3705492313837c",
                fabricanteId: Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                asraeId: 1,
                tipoProductoId: null
            ));

            await _productoRepository.InsertAsync(new Producto
            (
                id: Guid.Parse("466775b9-3409-4237-9871-5725a0947eb5"),
                noProducto: 579696757,
                nombreComercia: "646ef38b07ab442dbbff0e9ed39d8bb348f01411f96a410fa09081c89a376a63990107732159439d800d03d6dd",
                uso: "7373e5c7400b46bfb5370d8feea44550cdba9d691ee14d47911ca8414f4b35ff43db76835f7b4890a8264965dc3d315833e54bfd1f7b47a789dd08bee90f1192f6b3da87b394400c9d1d3048cffe4847a83e15e859b44a5fbd73602f8473e8750cf50f56",
                fabricanteId: Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                asraeId: 2,
                tipoProductoId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}