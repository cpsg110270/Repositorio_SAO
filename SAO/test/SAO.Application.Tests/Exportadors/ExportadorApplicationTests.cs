using Shouldly;
using System;
using System.Linq;
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
            result.Items.Any(x => x.Id == Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("3ce1ea30-6071-4a85-a84e-6a2074fd3373")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _exportadorsAppService.GetAsync(Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ExportadorCreateDto
            {
                NombreExportador = "4d8fad2bf0fd4df0b55737d3d469769ef61f011b73e147eb8640d50c7117060ee2570ecc8ca9425f88d8f63de39211b4376beca8fb6b471daa0a3b79cc115185dd32ad5030364f5c919acfa8b31efac883774b7edb114e9abb45a13afa3d248a435d624ccc524c25a08e21cca21ca42b062a8ffff3cb45c595c390652b"
            };

            // Act
            var serviceResult = await _exportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreExportador.ShouldBe("4d8fad2bf0fd4df0b55737d3d469769ef61f011b73e147eb8640d50c7117060ee2570ecc8ca9425f88d8f63de39211b4376beca8fb6b471daa0a3b79cc115185dd32ad5030364f5c919acfa8b31efac883774b7edb114e9abb45a13afa3d248a435d624ccc524c25a08e21cca21ca42b062a8ffff3cb45c595c390652b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ExportadorUpdateDto()
            {
                NombreExportador = "702d8f262e09447dbff3f6caa232c3841ae207bf7a694852a0ac791893f925acc14bb7e0bd224462a41c553fa72f7aae4b90ebe37ae843b1ab055a60871b9c32c094ee04a0804ab3b8abfe28210911c0780d3f4f2d7d4e8884304b5efb1a6b711626353d0c204532a0a67ed899ec3fd74e93e7d6f1fd435eab187a7645"
            };

            // Act
            var serviceResult = await _exportadorsAppService.UpdateAsync(Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"), input);

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreExportador.ShouldBe("702d8f262e09447dbff3f6caa232c3841ae207bf7a694852a0ac791893f925acc14bb7e0bd224462a41c553fa72f7aae4b90ebe37ae843b1ab055a60871b9c32c094ee04a0804ab3b8abfe28210911c0780d3f4f2d7d4e8884304b5efb1a6b711626353d0c204532a0a67ed899ec3fd74e93e7d6f1fd435eab187a7645");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _exportadorsAppService.DeleteAsync(Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"));

            // Assert
            var result = await _exportadorRepository.FindAsync(c => c.Id == Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"));

            result.ShouldBeNull();
        }
    }
}