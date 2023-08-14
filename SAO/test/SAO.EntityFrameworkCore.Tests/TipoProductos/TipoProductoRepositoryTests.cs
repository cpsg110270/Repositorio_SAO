using SAO.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.TipoProductos
{
    public class TipoProductoRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ITipoProductoRepository _tipoProductoRepository;

        public TipoProductoRepositoryTests()
        {
            _tipoProductoRepository = GetRequiredService<ITipoProductoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _tipoProductoRepository.GetListAsync(
                    desProducto: "f504d2854c814153b194"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _tipoProductoRepository.GetCountAsync(
                    desProducto: "f0cd8fc1bf68494c8168"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}