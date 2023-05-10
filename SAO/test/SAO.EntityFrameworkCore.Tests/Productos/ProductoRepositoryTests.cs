using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using SAO.Productos;
using SAO.EntityFrameworkCore;
using Xunit;

namespace SAO.Productos
{
    public class ProductoRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoRepositoryTests()
        {
            _productoRepository = GetRequiredService<IProductoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productoRepository.GetListAsync(
                    nombreComercia: "58bba7c256ef410bb6edcb3133f706a730d58dcb2bef4c8f80d3aacda98e1effab31d809051e40a98401d7a57d",
                    uso: "545a1743eb0a49a88d09522f753837c0c74c7c8bc56f4f7b8192449cd4d1ef141d70ee885be44bcaa4d562d8212325cf6aaad0071357484baea5530a00d4f61c22f65f3126624f8e96ee0c07276499e767b438091b7f457eba34af28fce0a831761129d5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productoRepository.GetCountAsync(
                    nombreComercia: "43aa8902827f4e47af95c3ba0df93b10c416c0ea343441faa3e2b0bef7951760dddae64b1f1e43a18807e53183",
                    uso: "a8d4ff9d9043467588bcadc8403696d6ed01f678006d498294a824ae24d78b9b312939618ef94f1fa7c7412ad164f7a7f0ac6d5a2df34e9b843a0eeb723658ec61f9f402824f42c2991d02a819c50389085a05e810634e1482808956760a870939463234"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}