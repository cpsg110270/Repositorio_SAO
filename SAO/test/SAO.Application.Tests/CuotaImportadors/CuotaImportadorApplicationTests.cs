using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorsAppServiceTests : SAOApplicationTestBase
    {
        private readonly ICuotaImportadorsAppService _cuotaImportadorsAppService;
        private readonly IRepository<CuotaImportador, Guid> _cuotaImportadorRepository;

        public CuotaImportadorsAppServiceTests()
        {
            _cuotaImportadorsAppService = GetRequiredService<ICuotaImportadorsAppService>();
            _cuotaImportadorRepository = GetRequiredService<IRepository<CuotaImportador, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetListAsync(new GetCuotaImportadorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c")).ShouldBe(true);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("298e7061-ab4f-4c2c-a631-122a8e5157fb")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetAsync(Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CuotaImportadorCreateDto
            {
                Año = 81942612,
                Cuota = 385432084,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                AsraeId = 1
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(81942612);
            result.Cuota.ShouldBe(385432084);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CuotaImportadorUpdateDto()
            {
                Año = 767718782,
                Cuota = 1678347853,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                AsraeId = 1
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.UpdateAsync(Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"), input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(767718782);
            result.Cuota.ShouldBe(1678347853);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cuotaImportadorsAppService.DeleteAsync(Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"));

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"));

            result.ShouldBeNull();
        }
    }
}