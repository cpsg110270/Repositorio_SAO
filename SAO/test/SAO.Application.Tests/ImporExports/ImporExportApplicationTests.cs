using System;
using System.Linq;
using Shouldly;
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
            result.Items.Any(x => x.ImporExport.Id == Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb")).ShouldBe(true);
            result.Items.Any(x => x.ImporExport.Id == Guid.Parse("6b5d90e7-4b57-48b2-b56a-3ccdaf0e4e13")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _imporExportsAppService.GetAsync(Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ImporExportCreateDto
            {
                NoPermiso = "85df7ef0",
                FechaEmision = new DateTime(2009, 11, 19),
                FechaSolicitud = new DateTime(2003, 1, 18),
                PesoNeto = 1357355406,
                PesoUnitario = 2004728162,
                CantEnvvase = 1698894618,
                NoFactura = "b9f9d44db03d4df58bbe2092b0034e",
                Observaciones = "7a07faa96b7f4eb6b47c483ca6332df375cd5575e6164d46933445747116c392e5919c5140854085894d004072b7e925bf33496ae21648ce91ee5ac8c357742ba5a01b98c87a4386b2c92f1f7db7d92ce17aefec0fde481c92658c196673c00f8b76cc91ece945aa9b4ac167187392023086a1919cce47a59b54550db07279adf019c4f1e8c445b2863e2354cace76038490dd2b5716",
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
            result.NoPermiso.ShouldBe("85df7ef0");
            result.FechaEmision.ShouldBe(new DateTime(2009, 11, 19));
            result.FechaSolicitud.ShouldBe(new DateTime(2003, 1, 18));
            result.PesoNeto.ShouldBe(1357355406);
            result.PesoUnitario.ShouldBe(2004728162);
            result.CantEnvvase.ShouldBe(1698894618);
            result.NoFactura.ShouldBe("b9f9d44db03d4df58bbe2092b0034e");
            result.Observaciones.ShouldBe("7a07faa96b7f4eb6b47c483ca6332df375cd5575e6164d46933445747116c392e5919c5140854085894d004072b7e925bf33496ae21648ce91ee5ac8c357742ba5a01b98c87a4386b2c92f1f7db7d92ce17aefec0fde481c92658c196673c00f8b76cc91ece945aa9b4ac167187392023086a1919cce47a59b54550db07279adf019c4f1e8c445b2863e2354cace76038490dd2b5716");
            result.EsRenovacion.ShouldBe(true);
            result.Estado.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ImporExportUpdateDto()
            {
                NoPermiso = "ecc08746",
                FechaEmision = new DateTime(2019, 10, 7),
                FechaSolicitud = new DateTime(2008, 9, 19),
                PesoNeto = 330109198,
                PesoUnitario = 195143717,
                CantEnvvase = 409261273,
                NoFactura = "5642092d1c3d42aebf88d265133763",
                Observaciones = "691d62dc90014b0986de20233c8aaa72d049994364ef4374a20ebbe34cd5faa8a11289654b4143fdbaa017a289965dc87e4e5580997e403983105c948a636d22fdbbf1210b9846c6af1d5035488682f40c498774029d4803af2dbe35340aa0c12739bd3b67054603baedf64811c784d9c5dbffc342374450badbf889e32db7fd117a409470c545fd8bedec3db2d627da34033f17d6cf",
                EsRenovacion = true,
                Estado = true,
                ExportadorId = Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                ProductoId = Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                UnidadMedidaId = 1,
                TipoEnvaseId = 1,
                PermisoDe = Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")
            };

            // Act
            var serviceResult = await _imporExportsAppService.UpdateAsync(Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"), input);

            // Assert
            var result = await _imporExportRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoPermiso.ShouldBe("ecc08746");
            result.FechaEmision.ShouldBe(new DateTime(2019, 10, 7));
            result.FechaSolicitud.ShouldBe(new DateTime(2008, 9, 19));
            result.PesoNeto.ShouldBe(330109198);
            result.PesoUnitario.ShouldBe(195143717);
            result.CantEnvvase.ShouldBe(409261273);
            result.NoFactura.ShouldBe("5642092d1c3d42aebf88d265133763");
            result.Observaciones.ShouldBe("691d62dc90014b0986de20233c8aaa72d049994364ef4374a20ebbe34cd5faa8a11289654b4143fdbaa017a289965dc87e4e5580997e403983105c948a636d22fdbbf1210b9846c6af1d5035488682f40c498774029d4803af2dbe35340aa0c12739bd3b67054603baedf64811c784d9c5dbffc342374450badbf889e32db7fd117a409470c545fd8bedec3db2d627da34033f17d6cf");
            result.EsRenovacion.ShouldBe(true);
            result.Estado.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _imporExportsAppService.DeleteAsync(Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"));

            // Assert
            var result = await _imporExportRepository.FindAsync(c => c.Id == Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"));

            result.ShouldBeNull();
        }
    }
}