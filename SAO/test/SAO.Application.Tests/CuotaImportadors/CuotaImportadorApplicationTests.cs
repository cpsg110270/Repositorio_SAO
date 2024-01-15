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
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55")).ShouldBe(true);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("052ee185-f673-4df4-9e35-9135d5dca813")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetAsync(Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CuotaImportadorCreateDto
            {
                Año = 914001302,
                Cuota = 1831900729,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),

            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(914001302);
            result.Cuota.ShouldBe(1831900729);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CuotaImportadorUpdateDto()
            {
                Año = 1242887230,
                Cuota = 239396708,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),

            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.UpdateAsync(Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"), input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(1242887230);
            result.Cuota.ShouldBe(239396708);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cuotaImportadorsAppService.DeleteAsync(Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"));

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"));

            result.ShouldBeNull();
        }
    }
}