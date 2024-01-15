using SAO.Web.Pages.TotalImportacioness;
using SAO.TotalImportacioness;
using AutoMapper;
using SAO.Almacens;
using SAO.Asraes;
using SAO.CuotaImportadors;
using SAO.Exportadors;
using SAO.Fabricantes;
using SAO.ImporExports;
using SAO.Importadors;
using SAO.Paiss;
using SAO.Productos;
using SAO.PuertoEntradaSalidas;
using SAO.SustanciaElementals;
using SAO.TipoEnvases;
using SAO.TipoPermisos;
using SAO.TipoProductos;
using SAO.UnidadMedidas;
using SAO.Web.Pages.Almacens;
using SAO.Web.Pages.Asraes;
using SAO.Web.Pages.CuotaImportadors;
using SAO.Web.Pages.Exportadors;
using SAO.Web.Pages.Fabricantes;
using SAO.Web.Pages.ImporExports;
using SAO.Web.Pages.Importadors;
using SAO.Web.Pages.Paiss;
using SAO.Web.Pages.Productos;
using SAO.Web.Pages.PuertoEntradaSalidas;
using SAO.Web.Pages.SustanciaElementals;
using SAO.Web.Pages.TipoEnvases;
using SAO.Web.Pages.TipoPermisos;
using SAO.Web.Pages.TipoProductos;
using SAO.Web.Pages.UnidadMedidas;
using Volo.Abp.AutoMapper;

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

        CreateMap<TotalImportacionesDto, TotalImportacionesUpdateViewModel>();
        CreateMap<TotalImportacionesUpdateViewModel, TotalImportacionesUpdateDto>();
        CreateMap<TotalImportacionesCreateViewModel, TotalImportacionesCreateDto>();
    }
}