using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Productos
{
    public class ProductosAppServiceTests : SAOApplicationTestBase
    {
        private readonly IProductosAppService _productosAppService;
        private readonly IRepository<Producto, Guid> _productoRepository;

        public ProductosAppServiceTests()
        {
            _productosAppService = GetRequiredService<IProductosAppService>();
            _productoRepository = GetRequiredService<IRepository<Producto, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _productosAppService.GetListAsync(new GetProductosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Producto.Id == Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63")).ShouldBe(true);
            result.Items.Any(x => x.Producto.Id == Guid.Parse("466775b9-3409-4237-9871-5725a0947eb5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _productosAppService.GetAsync(Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProductoCreateDto
            {
                NoProducto = 420778500,
                NombreComercia = "ec9d494d395c4299a25fe07bf07499330dd8a3db8bf947fb9afbd7e1d1d860e6530e83c37c1b4004a0ce9c3bdc",
                Uso = "c23e9cf43f154a2da6b2c61a9bf96af7bbdb9936b34748f0bf1abd964bf1aaaa27770c9f4cad4269a4c79ff3189e8d79f718b921f3a74201a65b7cca5380a145693ad9accdbf40e99dd122195abc95d6bc35923411a34217a00e83c770ab943143422fd9",
                FabricanteId = Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                AsraeId = 1,

            };

            // Act
            var serviceResult = await _productosAppService.CreateAsync(input);

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoProducto.ShouldBe(420778500);
            result.NombreComercia.ShouldBe("ec9d494d395c4299a25fe07bf07499330dd8a3db8bf947fb9afbd7e1d1d860e6530e83c37c1b4004a0ce9c3bdc");
            result.Uso.ShouldBe("c23e9cf43f154a2da6b2c61a9bf96af7bbdb9936b34748f0bf1abd964bf1aaaa27770c9f4cad4269a4c79ff3189e8d79f718b921f3a74201a65b7cca5380a145693ad9accdbf40e99dd122195abc95d6bc35923411a34217a00e83c770ab943143422fd9");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductoUpdateDto()
            {
                NoProducto = 586221995,
                NombreComercia = "6d85548c42b54bbc9bac88396530f565a6362cf8a3a64336895142229fc6f3b07e44d75c038e4e41a3cb39376d",
                Uso = "f70771f6bee343c297f7013abcf7d8c9e368ee216e2e4b9382e22a7f4f8843848cc285bab813419ab214a01935a11bbaea6a395a8afa41d086aa66e6758bcda172c49a04bcde4cffb4ce2e9d6f2b6fd09b07f573edbb4c6b99b1aefdd5d99eab1a489e4c",
                FabricanteId = Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                AsraeId = 1,

            };

            // Act
            var serviceResult = await _productosAppService.UpdateAsync(Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"), input);

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoProducto.ShouldBe(586221995);
            result.NombreComercia.ShouldBe("6d85548c42b54bbc9bac88396530f565a6362cf8a3a64336895142229fc6f3b07e44d75c038e4e41a3cb39376d");
            result.Uso.ShouldBe("f70771f6bee343c297f7013abcf7d8c9e368ee216e2e4b9382e22a7f4f8843848cc285bab813419ab214a01935a11bbaea6a395a8afa41d086aa66e6758bcda172c49a04bcde4cffb4ce2e9d6f2b6fd09b07f573edbb4c6b99b1aefdd5d99eab1a489e4c");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _productosAppService.DeleteAsync(Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"));

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == Guid.Parse("5338bf0f-ccbd-49d4-b14c-ccf54337da63"));

            result.ShouldBeNull();
        }
    }
}