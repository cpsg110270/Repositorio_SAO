using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Exportadors
{
    public class ExportadorsAppServiceTests : SAOApplicationTestBase
    {
        private readonly IExportadorsAppService _exportadorsAppService;
        private readonly IRepository<Exportador, Guid> _exportadorRepository;

        public ExportadorsAppServiceTests()
        {
            _exportadorsAppService = GetRequiredService<IExportadorsAppService>();
            _exportadorRepository = GetRequiredService<IRepository<Exportador, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _exportadorsAppService.GetListAsync(new GetExportadorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("edcb2c94-15f1-4d74-84e8-be72e1e6d87b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _exportadorsAppService.GetAsync(Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ExportadorCreateDto
            {
                NoImportador = 1457304762,
                NombreExportador = "bf692e8b45b94dc0a301dc2358c8c9810dfb8f434c044fadaff86a269c079276b6a25314af40438b8c59af4674dba5b438a420d3411841ab95d02c76e73a85161eb00e2730f04246bdee2d9571ac89536ca654eb12024600859595322517c8ed3e6ba5096b374102b9a8ba629f6ac6659d55c23422aa4f958feef5f313"
            };

            // Act
            var serviceResult = await _exportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoImportador.ShouldBe(1457304762);
            result.NombreExportador.ShouldBe("bf692e8b45b94dc0a301dc2358c8c9810dfb8f434c044fadaff86a269c079276b6a25314af40438b8c59af4674dba5b438a420d3411841ab95d02c76e73a85161eb00e2730f04246bdee2d9571ac89536ca654eb12024600859595322517c8ed3e6ba5096b374102b9a8ba629f6ac6659d55c23422aa4f958feef5f313");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ExportadorUpdateDto()
            {
                NoImportador = 617556795,
                NombreExportador = "3690fbbf02c6473798735d07eafc85461e3e7ff750aa49edb875a65e036b4b1d9f688df053a843e38cf631ceee5a0154b1fd434b27dc4f909f011326b1b2ef67ce1648699f9544809a652cf4407d10c3b14f6ee1a27c4e209c8d56bdfa5632eae10dadcae33c4a0785555cf32a0c0a47cf92b1c2b0164c3688f91bfb5a"
            };

            // Act
            var serviceResult = await _exportadorsAppService.UpdateAsync(Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"), input);

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoImportador.ShouldBe(617556795);
            result.NombreExportador.ShouldBe("3690fbbf02c6473798735d07eafc85461e3e7ff750aa49edb875a65e036b4b1d9f688df053a843e38cf631ceee5a0154b1fd434b27dc4f909f011326b1b2ef67ce1648699f9544809a652cf4407d10c3b14f6ee1a27c4e209c8d56bdfa5632eae10dadcae33c4a0785555cf32a0c0a47cf92b1c2b0164c3688f91bfb5a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _exportadorsAppService.DeleteAsync(Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"));

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"));

            result.ShouldBeNull();
        }
    }
}