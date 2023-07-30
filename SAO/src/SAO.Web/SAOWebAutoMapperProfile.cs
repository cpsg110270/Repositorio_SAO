using SAO.Web.Pages.CuotaImportadors;
using SAO.CuotaImportadors;
using SAO.Web.Pages.ImporExports;
using SAO.ImporExports;
using SAO.Web.Pages.TipoPermisos;
using SAO.TipoPermisos;
using SAO.Web.Pages.Productos;
using SAO.Productos;
using SAO.Web.Pages.Asraes;
using SAO.Asraes;
using SAO.Web.Pages.Almacens;
using SAO.Almacens;
using SAO.Web.Pages.Fabricantes;
using SAO.Fabricantes;
using SAO.Web.Pages.PuertoEntradaSalidas;
using SAO.PuertoEntradaSalidas;
using SAO.Web.Pages.Paiss;
using SAO.Paiss;
using SAO.Web.Pages.TipoEnvases;
using SAO.TipoEnvases;
using SAO.Web.Pages.UnidadMedidas;
using SAO.UnidadMedidas;
using SAO.Web.Pages.SustanciaElementals;
using SAO.SustanciaElementals;
using SAO.Web.Pages.TipoProductos;
using SAO.TipoProductos;
using SAO.Web.Pages.Exportadors;
using SAO.Exportadors;
using SAO.Web.Pages.Importadors;
using Volo.Abp.AutoMapper;
using SAO.Importadors;
using AutoMapper;
using SAO.Reportes;

namespace SAO.Web;

public class SAOWebAutoMapperProfile : Profile
{
    public SAOWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<ImportadorDto, ImportadorUpdateViewModel>();
        CreateMap<ImportadorUpdateViewModel, ImportadorUpdateDto>();
        CreateMap<ImportadorCreateViewModel, ImportadorCreateDto>();

        CreateMap<ExportadorDto, ExportadorUpdateViewModel>();
        CreateMap<ExportadorUpdateViewModel, ExportadorUpdateDto>();
        CreateMap<ExportadorCreateViewModel, ExportadorCreateDto>();

        CreateMap<TipoProductoDto, TipoProductoUpdateViewModel>();
        CreateMap<TipoProductoUpdateViewModel, TipoProductoUpdateDto>();
        CreateMap<TipoProductoCreateViewModel, TipoProductoCreateDto>();

        CreateMap<SustanciaElementalDto, SustanciaElementalUpdateViewModel>();
        CreateMap<SustanciaElementalUpdateViewModel, SustanciaElementalUpdateDto>();
        CreateMap<SustanciaElementalCreateViewModel, SustanciaElementalCreateDto>();

        CreateMap<UnidadMedidaDto, UnidadMedidaUpdateViewModel>();
        CreateMap<UnidadMedidaUpdateViewModel, UnidadMedidaUpdateDto>();
        CreateMap<UnidadMedidaCreateViewModel, UnidadMedidaCreateDto>();

        CreateMap<TipoEnvaseDto, TipoEnvaseUpdateViewModel>();
        CreateMap<TipoEnvaseUpdateViewModel, TipoEnvaseUpdateDto>();
        CreateMap<TipoEnvaseCreateViewModel, TipoEnvaseCreateDto>();

        CreateMap<PaisDto, PaisUpdateViewModel>();
        CreateMap<PaisUpdateViewModel, PaisUpdateDto>();
        CreateMap<PaisCreateViewModel, PaisCreateDto>();

        CreateMap<PuertoEntradaSalidaDto, PuertoEntradaSalidaUpdateViewModel>();
        CreateMap<PuertoEntradaSalidaUpdateViewModel, PuertoEntradaSalidaUpdateDto>();
        CreateMap<PuertoEntradaSalidaCreateViewModel, PuertoEntradaSalidaCreateDto>();

        CreateMap<FabricanteDto, FabricanteUpdateViewModel>();
        CreateMap<FabricanteUpdateViewModel, FabricanteUpdateDto>();
        CreateMap<FabricanteCreateViewModel, FabricanteCreateDto>();

        CreateMap<AlmacenDto, AlmacenUpdateViewModel>();
        CreateMap<AlmacenUpdateViewModel, AlmacenUpdateDto>();
        CreateMap<AlmacenCreateViewModel, AlmacenCreateDto>();

        CreateMap<AsraeDto, AsraeUpdateViewModel>();
        CreateMap<AsraeUpdateViewModel, AsraeUpdateDto>();
        CreateMap<AsraeCreateViewModel, AsraeCreateDto>();

        CreateMap<ProductoDto, ProductoUpdateViewModel>().Ignore(x => x.SustanciaElementalIds);
        CreateMap<ProductoUpdateViewModel, ProductoUpdateDto>();
        CreateMap<ProductoCreateViewModel, ProductoCreateDto>();

        CreateMap<TipoPermisoDto, TipoPermisoUpdateViewModel>();
        CreateMap<TipoPermisoUpdateViewModel, TipoPermisoUpdateDto>();
        CreateMap<TipoPermisoCreateViewModel, TipoPermisoCreateDto>();

        CreateMap<ImporExportDto, ImporExportUpdateViewModel>();
        CreateMap<ImporExportUpdateViewModel, ImporExportUpdateDto>();
        CreateMap<ImporExportCreateViewModel, ImporExportCreateDto>();

        CreateMap<CuotaImportadorDto, CuotaImportadorUpdateViewModel>();
        CreateMap<CuotaImportadorUpdateViewModel, CuotaImportadorUpdateDto>();
        CreateMap<CuotaImportadorCreateViewModel, CuotaImportadorCreateDto>();
    }
}