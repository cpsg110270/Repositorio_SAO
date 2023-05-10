using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Almacens
{
    public class AlmacensAppServiceTests : SAOApplicationTestBase
    {
        private readonly IAlmacensAppService _almacensAppService;
        private readonly IRepository<Almacen, int> _almacenRepository;

        public AlmacensAppServiceTests()
        {
            _almacensAppService = GetRequiredService<IAlmacensAppService>();
            _almacenRepository = GetRequiredService<IRepository<Almacen, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _almacensAppService.GetListAsync(new GetAlmacensInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _almacensAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AlmacenCreateDto
            {
                NombreAlmacen = "9653f11ac505418fb80dc7b7a29bd2b71ff5177718ca4fee8a5d3f7e371c469ceb44d27a54a14adb8230b8386b5ad476fcc8844f402e43809049653f79f9d52694c7009b079b41248a673f55be9c2b82392d426e59a4484f8f94bd614ed1ee14717c6f18",
                SiglaAlmacen = "f313c1949cac48e9b153"
            };

            // Act
            var serviceResult = await _almacensAppService.CreateAsync(input);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.NombreAlmacen == serviceResult.NombreAlmacen);

            result.ShouldNotBe(null);
            result.NombreAlmacen.ShouldBe("9653f11ac505418fb80dc7b7a29bd2b71ff5177718ca4fee8a5d3f7e371c469ceb44d27a54a14adb8230b8386b5ad476fcc8844f402e43809049653f79f9d52694c7009b079b41248a673f55be9c2b82392d426e59a4484f8f94bd614ed1ee14717c6f18");
            result.SiglaAlmacen.ShouldBe("f313c1949cac48e9b153");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AlmacenUpdateDto()
            {
                NombreAlmacen = "878c6350930041f6a135d2d92e7c4037d4d9e020c07b4030aa955fefd152b40be2f87aaa00194c7499ffd88bba4cf54dcbdf30a09ea346c390fc725d5e28edc49d285d25e0b74a5eac304938af00898b66718a2b313741a78ace3470b399bb8bae95a688",
                SiglaAlmacen = "2e6ce152294641de8913"
            };

            // Act
            var serviceResult = await _almacensAppService.UpdateAsync(1, input);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreAlmacen.ShouldBe("878c6350930041f6a135d2d92e7c4037d4d9e020c07b4030aa955fefd152b40be2f87aaa00194c7499ffd88bba4cf54dcbdf30a09ea346c390fc725d5e28edc49d285d25e0b74a5eac304938af00898b66718a2b313741a78ace3470b399bb8bae95a688");
            result.SiglaAlmacen.ShouldBe("2e6ce152294641de8913");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _almacensAppService.DeleteAsync(1);

            // Assert
            var result = await _almacenRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}