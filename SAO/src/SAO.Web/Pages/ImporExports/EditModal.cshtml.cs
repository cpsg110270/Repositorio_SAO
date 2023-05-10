using SAO.Shared;
using SAO.ImporExports;
using SAO.Productos;
using SAO.Exportadors;
using SAO.Importadors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using SAO.ImporExports;

namespace SAO.Web.Pages.ImporExports
{
    public class EditModalModel : SAOPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ImporExportUpdateViewModel ImporExport { get; set; }

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

            await _imporExportsAppService.UpdateAsync(Id, ObjectMapper.Map<ImporExportUpdateViewModel, ImporExportUpdateDto>(ImporExport));
            return NoContent();
        }
    }

    public class ImporExportUpdateViewModel : ImporExportUpdateDto
    {
    }
}