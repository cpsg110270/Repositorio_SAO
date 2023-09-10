using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAO.Exportadors;
using SAO.ImporExports;
using SAO.Importadors;
using SAO.Productos;
using SAO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SAO.Web.Pages.ImporExports
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ImporExportUpdateViewModel ImporExport { get; set; }
        public ImporExportWithNavigationPropertiesDto ImporExportWithNavigationPropertiesDto { get; set; }

        public ImportadorDto Importador { get; set; }
        public ExportadorDto Exportador { get; set; }
        public ProductoDto Producto { get; set; }
        public ImporExportDto ImporExport1 { get; set; }
        public List<SelectListItem> UnidadMedidaLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> TipoEnvaseLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> PuertoEntradaSalidaLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", null)
        };
        public List<SelectListItem> PaisLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", null)
        };
        public List<SelectListItem> AlmacenLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", null)
        };
        public List<SelectListItem> TipoPermisoLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly IImporExportsAppService _imporExportsAppService;

        public EditModalModel(IImporExportsAppService imporExportsAppService)
        {
            _imporExportsAppService = imporExportsAppService;

            ImporExport = new();
        }

        public async Task OnGetAsync()
        {
            var imporExportWithNavigationPropertiesDto = await _imporExportsAppService.GetWithNavigationPropertiesAsync(Id);
            ImporExportWithNavigationPropertiesDto = imporExportWithNavigationPropertiesDto;
            ImporExport = ObjectMapper.Map<ImporExportDto, ImporExportUpdateViewModel>(imporExportWithNavigationPropertiesDto.ImporExport);

            Importador = imporExportWithNavigationPropertiesDto.Importador;
            Exportador = imporExportWithNavigationPropertiesDto.Exportador;
            Producto = imporExportWithNavigationPropertiesDto.Producto;
            ImporExport1 = imporExportWithNavigationPropertiesDto.ImporExport1;
            UnidadMedidaLookupListRequired.AddRange((
                        await _imporExportsAppService.GetUnidadMedidaLookupAsync(new LookupRequestDto
                        {
                            MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                        })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );
            TipoEnvaseLookupListRequired.AddRange((
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
            TipoPermisoLookupListRequired.AddRange((
                                    await _imporExportsAppService.GetTipoPermisoLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {
            ImporExport.PesoNeto = ImporExport.PesoUnitario * ImporExport.CantEnvvase;
            ImporExport.PesoNeto = Math.Round(ImporExport.PesoNeto, 2);
            ImporExport.Estado = true;

            if (ImporExport.EsRenovacion && ImporExport.PermisoRenov == default)
                throw new UserFriendlyException("Debe seleccionar el Permiso a reemplazar");


            await _imporExportsAppService.UpdateAsync(Id, ObjectMapper.Map<ImporExportUpdateViewModel, ImporExportUpdateDto>(ImporExport));
            return NoContent();
        }
    }

    public class ImporExportUpdateViewModel : ImporExportUpdateDto
    {
    }
}