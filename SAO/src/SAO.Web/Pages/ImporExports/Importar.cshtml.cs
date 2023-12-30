using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using SAO.ImporExports;

namespace SAO.Web.Pages.ImporExports
{
    public class ImportarModel : SAOPageModel
    {
        [BindProperty]
        public IFormFile XlsFile { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public List<ImporExportCreateDto> ListaLicencias { get; set; } = new List<ImporExportCreateDto>();

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostImport()
        {
            ErrorMessage = null;

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

                var stream = XlsFile.OpenReadStream();

                //After save excel file in wwwroot and then
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        //return or alert message here
                        ErrorMessage = "Ha ocurrido un error! No se carga  el archivo";
                    }
                    else
                    {
                        //read excel file data and add data in  model.StaffInfoViewModel.StaffList
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string Permiso  = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
                            string FechaE   = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();
                            string FechaS   = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim();
                            string PesoN    = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(); 
                            string PesoU    = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(); 
                            string CantEnv  = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim(); 
                            string Factura  = (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim(); 
                            string Obser    = (worksheet.Cells[row, 8].Value ?? string.Empty).ToString().Trim(); 
                            string esRen    = (worksheet.Cells[row, 9].Value ?? string.Empty).ToString().Trim(); 
                            string estado   = (worksheet.Cells[row, 10].Value ?? string.Empty).ToString().Trim(); 
                            string Impor    = (worksheet.Cells[row, 11].Value ?? string.Empty).ToString().Trim(); 
                            string Expor    = (worksheet.Cells[row, 12].Value ?? string.Empty).ToString().Trim(); 
                            string Product  = (worksheet.Cells[row, 13].Value ?? string.Empty).ToString().Trim(); 
                            string MedidaU  = (worksheet.Cells[row, 14].Value ?? string.Empty).ToString().Trim(); 
                            string EnvTipo  = (worksheet.Cells[row, 15].Value ?? string.Empty).ToString().Trim(); 
                            string PuertoE  = (worksheet.Cells[row, 16].Value ?? string.Empty).ToString().Trim(); 
                            string PuertoS  = (worksheet.Cells[row, 17].Value ?? string.Empty).ToString().Trim(); 
                            string PaisP    = (worksheet.Cells[row, 18].Value ?? string.Empty).ToString().Trim(); 
                            string PaisD    = (worksheet.Cells[row, 19].Value ?? string.Empty).ToString().Trim(); 
                            string PaisO    = (worksheet.Cells[row, 20].Value ?? string.Empty).ToString().Trim(); 
                            string Almacen  = (worksheet.Cells[row, 21].Value ?? string.Empty).ToString().Trim(); 
                            string PermisoR = (worksheet.Cells[row, 22].Value ?? string.Empty).ToString().Trim();

                            if (esRen == "0")
                            { esRen = "False"; }
                            else { esRen = "True"; }

                            if (estado == "0")
                            { estado = "False"; }
                            else { estado = "True"; }

                            if (PermisoR == "")
                            { PermisoR = "NULL"; }


                            ListaLicencias.Add(new ImporExportCreateDto
                            {
                                NoPermiso = Permiso,
                                FechaEmision = DateTime.Parse(FechaE),
                                FechaSolicitud = DateTime.Parse(FechaS),
                                PesoNeto = Double.Parse(PesoN),
                                PesoUnitario = Double.Parse(PesoU),
                                CantEnvvase = int.Parse(CantEnv),
                                NoFactura = Factura,
                                Observaciones = Obser,
                                EsRenovacion = bool.Parse(esRen),
                                Estado = bool.Parse(estado),
                                ImportadorId = Guid.Parse(Impor),
                                ExportadorId = Guid.Parse(Expor),
                                ProductoId = Guid.Parse(Product),
                                UnidadMedidaId = int.Parse(MedidaU),
                                TipoEnvaseId = int.Parse(EnvTipo),
                                PuertoEntradaId = int.Parse(PuertoE),
                                PuertoSalidaId = int.Parse(PuertoS),
                                PaisProcedenciaId = int.Parse(PaisP),
                                PaisDestinoId = int.Parse(PaisD),
                                PaisOrigenId = int.Parse(PaisO),
                                AlmacenId = int.Parse(Almacen),
                                //PermisoRenov=Guid.Parse(PermisoR)


                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ha ocurrido un error! " + ex.Message;
            }

            //return same view and pass view model
            return Page();
        }
    }
}