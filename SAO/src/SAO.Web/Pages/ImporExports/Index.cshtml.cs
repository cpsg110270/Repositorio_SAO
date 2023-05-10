using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using SAO.ImporExports;
using SAO.Shared;

namespace SAO.Web.Pages.ImporExports
{
    public class IndexModel : AbpPageModel
    {
        public string? NoPermisoFilter { get; set; }
        public DateTime? FechaEmisionFilterMin { get; set; }

        public DateTime? FechaEmisionFilterMax { get; set; }
        public DateTime? FechaSolicitudFilterMin { get; set; }

        public DateTime? FechaSolicitudFilterMax { get; set; }
        public double? PesoNetoFilterMin { get; set; }

        public double? PesoNetoFilterMax { get; set; }
        public double? PesoUnitarioFilterMin { get; set; }

        public double? PesoUnitarioFilterMax { get; set; }
        public int? CantEnvvaseFilterMin { get; set; }

        public int? CantEnvvaseFilterMax { get; set; }
        public string? NoFacturaFilter { get; set; }
        public string? ObservacionesFilter { get; set; }
        [SelectItems(nameof(EsRenovacionBoolFilterItems))]
        public string EsRenovacionFilter { get; set; }

        public List<SelectListItem> EsRenovacionBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(EstadoBoolFilterItems))]
        public string EstadoFilter { get; set; }

        public List<SelectListItem> EstadoBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ImportadorLookupList))]
        public Guid? ImportadorIdFilter { get; set; }
        public List<SelectListItem> ImportadorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ExportadorLookupList))]
        public Guid ExportadorIdFilter { get; set; }
        public List<SelectListItem> ExportadorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ProductoLookupList))]
        public Guid ProductoIdFilter { get; set; }
        public List<SelectListItem> ProductoLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(UnidadMedidaLookupList))]
        public int UnidadMedidaIdFilter { get; set; }
        public List<SelectListItem> UnidadMedidaLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(TipoEnvaseLookupList))]
        public int TipoEnvaseIdFilter { get; set; }
        public List<SelectListItem> TipoEnvaseLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(PuertoEntradaSalidaLookupList))]
        public int? PuertoEntradaIdFilter { get; set; }
        public List<SelectListItem> PuertoEntradaSalidaLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(PuertoEntradaSalidaLookupList))]
        public int? PuertoSalidaIdFilter { get; set; }
        [SelectItems(nameof(PaisLookupList))]
        public int? PaisProcedenciaIdFilter { get; set; }
        public List<SelectListItem> PaisLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(PaisLookupList))]
        public int? PaisDestinoIdFilter { get; set; }
        [SelectItems(nameof(PaisLookupList))]
        public int? PaisOrigenIdFilter { get; set; }
        [SelectItems(nameof(AlmacenLookupList))]
        public int? AlmacenIdFilter { get; set; }
        public List<SelectListItem> AlmacenLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, null)
        };

        [SelectItems(nameof(ImporExportLookupList))]
        public Guid? PermisoRenovFilter { get; set; }
        public List<SelectListItem> ImporExportLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(TipoPermisoLookupList))]
        public Guid PermisoDeFilter { get; set; }
        public List<SelectListItem> TipoPermisoLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IImporExportsAppService _imporExportsAppService;

        public IndexModel(IImporExportsAppService imporExportsAppService)
        {
            _imporExportsAppService = imporExportsAppService;
        }

        public async Task OnGetAsync()
        {
            ImportadorLookupList.AddRange((
                    await _imporExportsAppService.GetImportadorLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            ExportadorLookupList.AddRange((
                            await _imporExportsAppService.GetExportadorLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            ProductoLookupList.AddRange((
                            await _imporExportsAppService.GetProductoLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            UnidadMedidaLookupList.AddRange((
                            await _imporExportsAppService.GetUnidadMedidaLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            TipoEnvaseLookupList.AddRange((
                            await _imporExportsAppService.GetTipoEnvaseLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            PuertoEntradaSalidaLookupList.AddRange((
                            await _imporExportsAppService.GetPuertoEntradaSalidaLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            PaisLookupList.AddRange((
                            await _imporExportsAppService.GetPaisLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            AlmacenLookupList.AddRange((
                            await _imporExportsAppService.GetAlmacenLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            ImporExportLookupList.AddRange((
                            await _imporExportsAppService.GetImporExportLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            TipoPermisoLookupList.AddRange((
                            await _imporExportsAppService.GetTipoPermisoLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}