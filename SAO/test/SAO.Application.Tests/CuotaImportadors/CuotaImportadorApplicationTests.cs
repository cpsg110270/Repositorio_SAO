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
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23")).ShouldBe(true);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("b957dc8f-fc60-4d39-8296-07639568a9ae")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetAsync(Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CuotaImportadorCreateDto
            {
                Año = 1219464437,
                Cuota = 328707030,
                ImportadorId = Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37")
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(1219464437);
            result.Cuota.ShouldBe(328707030);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CuotaImportadorUpdateDto()
            {
                Año = 2089345013,
                Cuota = 1964732662,
                ImportadorId = Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37")
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.UpdateAsync(Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"), input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(2089345013);
            result.Cuota.ShouldBe(1964732662);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cuotaImportadorsAppService.DeleteAsync(Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"));

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"));

            result.ShouldBeNull();
        }
    }
}