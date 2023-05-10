using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.TipoProductos
{
    public class TipoProductosAppServiceTests : SAOApplicationTestBase
    {
        private readonly ITipoProductosAppService _tipoProductosAppService;
        private readonly IRepository<TipoProducto, Guid> _tipoProductoRepository;

        public TipoProductosAppServiceTests()
        {
            _tipoProductosAppService = GetRequiredService<ITipoProductosAppService>();
            _tipoProductoRepository = GetRequiredService<IRepository<TipoProducto, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _tipoProductosAppService.GetListAsync(new GetTipoProductosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b6732dd0-cd26-40aa-ba08-a784f000255f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _tipoProductosAppService.GetAsync(Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TipoProductoCreateDto
            {
                DesProducto = "c33514eb3330446ba6b2"
            };

            // Act
            var serviceResult = await _tipoProductosAppService.CreateAsync(input);

            // Assert
            var result = await _tipoProductoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DesProducto.ShouldBe("c33514eb3330446ba6b2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TipoProductoUpdateDto()
            {
                DesProducto = "54f96541e1bc4b678715"
            };

            // Act
            var serviceResult = await _tipoProductosAppService.UpdateAsync(Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"), input);

            // Assert
            var result = await _tipoProductoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DesProducto.ShouldBe("54f96541e1bc4b678715");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _tipoProductosAppService.DeleteAsync(Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"));

            // Assert
            var result = await _tipoProductoRepository.FindAsync(c => c.Id == Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"));

            result.ShouldBeNull();
        }
    }
}