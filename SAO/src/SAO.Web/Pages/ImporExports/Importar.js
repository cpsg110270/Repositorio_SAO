 

//Loader...
$(document).ajaxStart(function () {
    abp.ui.setBusy();
});

$(document).ajaxStop(function () {
    abp.ui.clearBusy();
});

$(document).ajaxError(function () {
    abp.ui.clearBusy();
});

/* show file value after file select */
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var fileName = document.getElementById("chooseFile").files[0].name;
    var nextSibling = e.target.nextElementSibling
    nextSibling.innerText = fileName
})

 


$("#GuardarLicenciasButton").click(function () {


   /* $("#tableLicencias > tbody > tr").each(function (i, tr) {*/
    $.each($("#tableLicencias tbody tr"), function (i, item) {
        
        var Permiso = $(this).find('td:eq(0)').html().toString().trim();
        var FechaE  = $(this).find('td:eq(1)').html().toString().trim();
        var FechaS  = $(this).find('td:eq(2)').html().toString().trim();
        var PesoN = $(this).find('td:eq(3)').html().toString().trim();
        var Impor = $(this).find('td:eq(4)').html().toString().trim();
        var Expor = $(this).find('td:eq(5)').html().toString().trim();
        var Product = $(this).find('td:eq(6)').html().toString().trim();
        var PesoU   = $(this).find('td:eq(7)').html().toString().trim();
        var CanEnv = $(this).find('td:eq(8)').html().toString().trim();
        var Factura = $(this).find('td:eq(9)').html().toString().trim();
        var Obser   = $(this).find('td:eq(10)').html().toString().trim();
        var esRen   = $(this).find('td:eq(11)').html();
        var estado = $(this).find('td:eq(12)').html();
        var MedidaU = $(this).find('td:eq(13)').html().toString().trim();
        var EnvTipo = $(this).find('td:eq(14)').html().toString().trim();
        var PuertoE = $(this).find('td:eq(15)').html().toString().trim();
        var PuertoS = $(this).find('td:eq(16)').html().toString().trim();
        var PaisP   = $(this).find('td:eq(17)').html().toString().trim();
        var PaisD   = $(this).find('td:eq(18)').html().toString().trim();
        var PaisO   = $(this).find('td:eq(19)').html().toString().trim();
        var Almacen = $(this).find('td:eq(20)').html().toString().trim();
        var PermisoR = $(this).find('td:eq(21)').html().toString().trim();
    
       
        abp.ajax({
            url: abp.appPath + 'api/app/impor-exports',
            type: 'POST',
            abpHandleError: false, //DISABLE AUTO ERROR HANDLING

            /*data: JSON.stringify({
                NoPemiso: Permiso,
                FechaEmision: FechaE,
                FechaSolicitud: FechaS,
                PesoNeto: PesoN,
                PesoUnitario: PesoU,
                CantEnnvase: CanEnv,
                NoFactura: Factura,
                Observaciones : Obser,
                EsRenovacion : esRen,
                Estado : estado ,
                ImportadorId : Impor,
                ExportadorId : Expor,
                ProductoId : Product,
                UnidadMedidaId : MedidaU,
                TipoEnvaseId : EnvTipo,
                PuertoEntradaId : PuertoE,
                PuertoSalidaId : PuertoS,
                PaisProcedenciaId : PaisP,
                PaisDestinoId : PaisD,
                PaisOrigenId : PaisO,
                AlmacenId : Almacen,
                PermisoRenov :PermisoR

            }),*/
            /*data: JSON.stringify({
                NoPermiso: "string",
                FechaEmision: "2024-01-15T04:53:50.056Z",
                FechaSolicitud: "2024-01-15T04:53:50.056Z",
                PesoNeto: 0,
                PesoUnitario: 0,
                CantEnvvase: 0,
                NoFactura: "string",
                Observaciones: "string",
                EsRenovacion: true,
                Estado: true,
                ImportadorId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                ExportadorId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                ProductoId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                UnidadMedidaId: 0,
                TipoEnvaseId: 0,
                PuertoEntradaId: 0,
                PuertoSalidaId: 0,
                PaisProcedenciaId: 0,
                PaisDestinoId: 0,
                PaisOrigenId: 0,
                AlmacenId: 0,
                PermisoRenov: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                PermisoDe: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            }),*/
            data: JSON.stringify({
                NoPermiso: Permiso,
                FechaEmision: FechaE,
                FechaSolicitud: FechaS,
                PesoNeto: parseFloat(PesoN),
                ImportadorId: Impor,
                ExportadorId: Expor,
                ProductoId: Product,
                PesoUnitario: parseFloat(PesoU),
                CantEnvvase: parseFloat(CanEnv),
                NoFactura: Factura,
                Observaciones: Obser,
                EsRenovacion: esRen === "true" ? true : false,
                Estado: estado === "true" ? true : false,
                UnidadMedidaId: parseInt(MedidaU),
                TipoEnvaseId: parseInt(EnvTipo),
                PuertoEntradaId: parseInt(PuertoE),
                PuertoSalidaId: parseInt(PuertoS),
                PaisProcedenciaId: parseInt(PaisP),
                PaisDestinoId: parseInt(PaisD),
                PaisOrigenId: parseInt(PaisO),
                AlmacenId: parseInt(Almacen),
                PermisoRenov: PermisoR
            }),
            success: function (data) {
                $(item).find('td:eq(2)').html('<span class="badge badge-success">Éxito</span>');
                $(item).find('td:eq(2)').attr('data-original-title', 'Registro guardado con Éxito!');
            },
            error: function (error) {
                if (error.code == 'Exist') {
                    $(item).find('td:eq(2)').html('<span class="badge badge-warning">Existe</span>');
                    $(item).find('td:eq(2)').attr('data-original-title', error.message);
                }
                else {
                    $(item).find('td:eq(2)').html('<span class="badge badge-danger">Error</span>');
                    $(item).find('td:eq(2)').attr('data-original-title', error.message);
                }
            }
        });
    });
});