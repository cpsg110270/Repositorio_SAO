using Volo.Abp.AutoMapper;
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
using SAO.Reportes;
using SAO.Shared;
using SAO.SustanciaElementals;
using SAO.TipoEnvases;
using SAO.TipoPermisos;
using SAO.TipoProductos;
using SAO.UnidadMedidas;
using System;

namespace SAO;

public class SAOApplicationAutoMapperProfile : Profile
{
    public SAOApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Importador, ImportadorDto>();
        CreateMap<Importador, ImportadorExcelDto>();

        CreateMap<Exportador, ExportadorDto>();
        CreateMap<Exportador, ExportadorExcelDto>();

        CreateMap<TipoProducto, TipoProductoDto>();
        CreateMap<TipoProducto, TipoProductoExcelDto>();

        CreateMap<SustanciaElemental, SustanciaElementalDto>();
        CreateMap<SustanciaElemental, SustanciaElementalExcelDto>();

        CreateMap<UnidadMedida, UnidadMedidaDto>();
        CreateMap<UnidadMedida, UnidadMedidaExcelDto>();

        CreateMap<TipoEnvase, TipoEnvaseDto>();
        CreateMap<TipoEnvase, TipoEnvaseExcelDto>();

        CreateMap<Pais, PaisDto>();
        CreateMap<Pais, PaisExcelDto>();

        CreateMap<PuertoEntradaSalida, PuertoEntradaSalidaDto>();
        CreateMap<PuertoEntradaSalida, PuertoEntradaSalidaExcelDto>();

        CreateMap<Fabricante, FabricanteDto>();
        CreateMap<Fabricante, FabricanteExcelDto>();

        CreateMap<Almacen, AlmacenDto>();
        CreateMap<Almacen, AlmacenExcelDto>();

        CreateMap<Asrae, AsraeDto>();
        CreateMap<Asrae, AsraeExcelDto>();

        CreateMap<Producto, ProductoDto>();
        CreateMap<Producto, ProductoExcelDto>();
        CreateMap<ProductoWithNavigationProperties, ProductoWithNavigationPropertiesDto>();
        CreateMap<Fabricante, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombreFabricante));
        //CreateMap<SustanciaElemental, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.CodCas));
        CreateMap<SustanciaElemental, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Completo));

        CreateMap<Asrae, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Codigo_ASHRAE));

        CreateMap<TipoPermiso, TipoPermisoDto>();
        CreateMap<TipoPermiso, TipoPermisoExcelDto>();

        CreateMap<TipoProducto, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DesProducto));

        CreateMap<ImporExport, ImporExportDto>();
        CreateMap<ImporExport, ImporExportExcelDto>();
        CreateMap<ImporExportWithNavigationProperties, ImporExportWithNavigationPropertiesDto>();
        CreateMap<Importador, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombreImportador));
        CreateMap<Exportador, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombreExportador));
        CreateMap<Producto, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombreComercia));
        CreateMap<UnidadMedida, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Abreviatura));
        CreateMap<TipoEnvase, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DesEnvase));
        CreateMap<PuertoEntradaSalida, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombrePuerto));
        CreateMap<PuertoEntradaSalida, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombrePuerto));
        CreateMap<Pais, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombrePais));
        CreateMap<Pais, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombrePais));
        CreateMap<Pais, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombrePais));
        CreateMap<Almacen, LookupDto<int>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NombreAlmacen));

        CreateMap<ImporExport, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NoPermiso));

        CreateMap<TipoPermiso, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Desripcion));

        CreateMap<CuotaImportador, CuotaImportadorDto>();
        CreateMap<CuotaImportador, CuotaImportadorExcelDto>();
        CreateMap<CuotaImportadorWithNavigationProperties, CuotaImportadorWithNavigationPropertiesDto>();

        CreateMap<RepCuotasImportadores, RepCuotasImportadoresDto>();
        CreateMap<RepPesosNetosASHRAE, RepPesosNetosASHRAEDto>();
    }
}