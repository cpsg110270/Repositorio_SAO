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
                id: Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                nombreComercia: "58bba7c256ef410bb6edcb3133f706a730d58dcb2bef4c8f80d3aacda98e1effab31d809051e40a98401d7a57d",
                uso: "545a1743eb0a49a88d09522f753837c0c74c7c8bc56f4f7b8192449cd4d1ef141d70ee885be44bcaa4d562d8212325cf6aaad0071357484baea5530a00d4f61c22f65f3126624f8e96ee0c07276499e767b438091b7f457eba34af28fce0a831761129d5",
                fabricanteId: Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                asraeId: 1,
                tipoProductoId: null
            ));

            await _productoRepository.InsertAsync(new Producto
            (
                id: Guid.Parse("ecc645bf-6504-4920-80a3-3edc9b48e3a6"),
                nombreComercia: "43aa8902827f4e47af95c3ba0df93b10c416c0ea343441faa3e2b0bef7951760dddae64b1f1e43a18807e53183",
                uso: "a8d4ff9d9043467588bcadc8403696d6ed01f678006d498294a824ae24d78b9b312939618ef94f1fa7c7412ad164f7a7f0ac6d5a2df34e9b843a0eeb723658ec61f9f402824f42c2991d02a819c50389085a05e810634e1482808956760a870939463234",
                fabricanteId: Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                asraeId: 2,
                tipoProductoId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}