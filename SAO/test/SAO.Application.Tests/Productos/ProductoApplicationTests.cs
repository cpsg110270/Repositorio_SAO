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
            result.Items.Any(x => x.Producto.Id == Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4")).ShouldBe(true);
            result.Items.Any(x => x.Producto.Id == Guid.Parse("ecc645bf-6504-4920-80a3-3edc9b48e3a6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _productosAppService.GetAsync(Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProductoCreateDto
            {
                NombreComercia = "3f6f918955a64ce9943d84b039ef873459cfc45ec826402e872cc67477b77e7259d48eacea284c2a8f9e8f8197",
                Uso = "2ea065c040cc487da75eaaea17c836281f1b07f857724d8f94d12355e9a19bc5d552e2150799427cba63f39c3d273424ae8312f15e614b2396b52eae97dda1bf7b65a7749fdf4d019f6c25539a5aae848a135551abfd4809b5109e5cf0afe85dbcd3fc8c",
                FabricanteId = Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                AsraeId = 1,

            };

            // Act
            var serviceResult = await _productosAppService.CreateAsync(input);

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreComercia.ShouldBe("3f6f918955a64ce9943d84b039ef873459cfc45ec826402e872cc67477b77e7259d48eacea284c2a8f9e8f8197");
            result.Uso.ShouldBe("2ea065c040cc487da75eaaea17c836281f1b07f857724d8f94d12355e9a19bc5d552e2150799427cba63f39c3d273424ae8312f15e614b2396b52eae97dda1bf7b65a7749fdf4d019f6c25539a5aae848a135551abfd4809b5109e5cf0afe85dbcd3fc8c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductoUpdateDto()
            {
                NombreComercia = "8a81a361aae546e0bf2f75083eb81f3e53e05e4f1ce1472785cd82c531213ca08368bd9db7244cb39c02ea3ed7",
                Uso = "23089f116ae949ada66a47ebb013b911985e5819c4fc483f88c3c3428ef3042deb36284020f2433795dbdf98ee5450ac5d302bcc89d14bfa9d542f439de5c2eeef1b3e4dfa2d4571b315d1ed5bf90bbc57f1d59920bd4efdbf872d13b26ce654aba470e7",
                FabricanteId = Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                AsraeId = 1,

            };

            // Act
            var serviceResult = await _productosAppService.UpdateAsync(Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"), input);

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreComercia.ShouldBe("8a81a361aae546e0bf2f75083eb81f3e53e05e4f1ce1472785cd82c531213ca08368bd9db7244cb39c02ea3ed7");
            result.Uso.ShouldBe("23089f116ae949ada66a47ebb013b911985e5819c4fc483f88c3c3428ef3042deb36284020f2433795dbdf98ee5450ac5d302bcc89d14bfa9d542f439de5c2eeef1b3e4dfa2d4571b315d1ed5bf90bbc57f1d59920bd4efdbf872d13b26ce654aba470e7");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _productosAppService.DeleteAsync(Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"));

            // Assert
            var result = await _productoRepository.FindAsync(c => c.Id == Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"));

            result.ShouldBeNull();
        }
    }
}