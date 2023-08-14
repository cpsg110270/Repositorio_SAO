using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.ImporExports
{
    public class ImporExportsAppServiceTests : SAOApplicationTestBase
    {
        private readonly IImporExportsAppService _imporExportsAppService;
        private readonly IRepository<ImporExport, Guid> _imporExportRepository;

        public ImporExportsAppServiceTests()
        {
            _imporExportsAppService = GetRequiredService<IImporExportsAppService>();
            _imporExportRepository = GetRequiredService<IRepository<ImporExport, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _imporExportsAppService.GetListAsync(new GetImporExportsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ImporExport.Id == Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947")).ShouldBe(true);
            result.Items.Any(x => x.ImporExport.Id == Guid.Parse("6faa3da5-873e-4002-a197-233d029685e8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _imporExportsAppService.GetAsync(Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ImporExportCreateDto
            {
                NoPermiso = "b8a37ba0",
                FechaEmision = new DateTime(2002, 9, 18),
                FechaSolicitud = new DateTime(2006, 3, 4),
                PesoNeto = 1257359780,
                PesoUnitario = 691320881,
                CantEnvvase = 1951089784,
                NoFactura = "70f4acf03ae8",
                Observaciones = "56545559b9254a30bb857bfbc485e7244b8d68d933d646ddb1ccf46d43b47cbc03a870de57e34ebebfb35a3997545c908aa939a9ae2a441eadffae23891a1237f0beba9d26dc4759a30a16f4f5ce68e99b62fd87740c4d52a6643f9760d61e65b8db69cb7c704ae59ee284e9687456c7162105896e114d4c8937693037a5264d5ec38065ab2743d9a38784991925aa9a76963dd9f7c6",
                EsRenovacion = true,
                Estado = true,
                ExportadorId = Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                ProductoId = Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                UnidadMedidaId = 1,
                TipoEnvaseId = 1,
                PermisoDe = Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")
            };

            // Act
            var serviceResult = await _imporExportsAppService.CreateAsync(input);

            // Assert
            var result = await _imporExportRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoPermiso.ShouldBe("b8a37ba0");
            result.FechaEmision.ShouldBe(new DateTime(2002, 9, 18));
            result.FechaSolicitud.ShouldBe(new DateTime(2006, 3, 4));
            result.PesoNeto.ShouldBe(1257359780);
            result.PesoUnitario.ShouldBe(691320881);
            result.CantEnvvase.ShouldBe(1951089784);
            result.NoFactura.ShouldBe("70f4acf03ae8");
            result.Observaciones.ShouldBe("56545559b9254a30bb857bfbc485e7244b8d68d933d646ddb1ccf46d43b47cbc03a870de57e34ebebfb35a3997545c908aa939a9ae2a441eadffae23891a1237f0beba9d26dc4759a30a16f4f5ce68e99b62fd87740c4d52a6643f9760d61e65b8db69cb7c704ae59ee284e9687456c7162105896e114d4c8937693037a5264d5ec38065ab2743d9a38784991925aa9a76963dd9f7c6");
            result.EsRenovacion.ShouldBe(true);
            result.Estado.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ImporExportUpdateDto()
            {
                NoPermiso = "961deb98",
                FechaEmision = new DateTime(2013, 5, 14),
                FechaSolicitud = new DateTime(2006, 11, 2),
                PesoNeto = 2094010798,
                PesoUnitario = 2129497411,
                CantEnvvase = 2048659944,
                NoFactura = "1bd75c873645",
                Observaciones = "e508a2b862cc4fab8d03d391b751b111c57a2f17dd7045abb531e7491d6c19383d1a568942914af1862ea857fbb0929e7fed625266c24c7bb9e079388f4cdafb913e54627cfc4d339a84986f5e82fbafa2863e35f540448ea2d782e0fb6b3aefbe266a788d94411c80718a518334c42839b3a06109384e658d43620c0585e75f35cff3952c804ad68e0e9df5e46af6a3431c427485ee",
                EsRenovacion = true,
                Estado = true,
                ExportadorId = Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                ProductoId = Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                UnidadMedidaId = 1,
                TipoEnvaseId = 1,
                PermisoDe = Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")
            };

            // Act
            var serviceResult = await _imporExportsAppService.UpdateAsync(Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"), input);

            // Assert
            var result = await _imporExportRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoPermiso.ShouldBe("961deb98");
            result.FechaEmision.ShouldBe(new DateTime(2013, 5, 14));
            result.FechaSolicitud.ShouldBe(new DateTime(2006, 11, 2));
            result.PesoNeto.ShouldBe(2094010798);
            result.PesoUnitario.ShouldBe(2129497411);
            result.CantEnvvase.ShouldBe(2048659944);
            result.NoFactura.ShouldBe("1bd75c873645");
            result.Observaciones.ShouldBe("e508a2b862cc4fab8d03d391b751b111c57a2f17dd7045abb531e7491d6c19383d1a568942914af1862ea857fbb0929e7fed625266c24c7bb9e079388f4cdafb913e54627cfc4d339a84986f5e82fbafa2863e35f540448ea2d782e0fb6b3aefbe266a788d94411c80718a518334c42839b3a06109384e658d43620c0585e75f35cff3952c804ad68e0e9df5e46af6a3431c427485ee");
            result.EsRenovacion.ShouldBe(true);
            result.Estado.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _imporExportsAppService.DeleteAsync(Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"));

            // Assert
            var result = await _imporExportRepository.FindAsync(c => c.Id == Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"));

            result.ShouldBeNull();
        }
    }
}