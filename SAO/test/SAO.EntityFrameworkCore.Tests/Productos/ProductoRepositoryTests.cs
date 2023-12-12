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
                    nombreComercia: "ac34139e76984a0296adbdb08a191f64c4d636e162564ec6a4d69293d72b5d4b0e807c81d4fd4d2f84645b246b",
                    uso: "cec430b57fd949288199bffd43954a63f31c2a4e6d504c0aa488e098ad0d58c44ba7d8965435426394b9ff2de05bb6d4bd0519c4827e405b87b1c167e2b2aad3b310beeb38034863940dbea79edaa8716ab43221deaf44ef9d905973de3705492313837c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"));
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
                    nombreComercia: "646ef38b07ab442dbbff0e9ed39d8bb348f01411f96a410fa09081c89a376a63990107732159439d800d03d6dd",
                    uso: "7373e5c7400b46bfb5370d8feea44550cdba9d691ee14d47911ca8414f4b35ff43db76835f7b4890a8264965dc3d315833e54bfd1f7b47a789dd08bee90f1192f6b3da87b394400c9d1d3048cffe4847a83e15e859b44a5fbd73602f8473e8750cf50f56"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}