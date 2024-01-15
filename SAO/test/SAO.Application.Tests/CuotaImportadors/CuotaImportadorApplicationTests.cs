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
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303")).ShouldBe(true);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("46fe4e12-301c-4aa7-84c3-81a4e4cbaaa1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetAsync(Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CuotaImportadorCreateDto
            {
                Año = 1050951241,
                Cuota = 373261600,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),

            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(1050951241);
            result.Cuota.ShouldBe(373261600);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CuotaImportadorUpdateDto()
            {
                Año = 2144386981,
                Cuota = 60086001,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),

            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.UpdateAsync(Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"), input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(2144386981);
            result.Cuota.ShouldBe(60086001);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cuotaImportadorsAppService.DeleteAsync(Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"));

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"));

            result.ShouldBeNull();
        }
    }
}