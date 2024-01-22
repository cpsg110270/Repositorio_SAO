 

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
   /* alert('en guardar');*/

  /*  $("#tableLicencias > tbody > tr").each(function (i, tr) {*/
    $.each($("#tableLicencias tbody tr"), function (i, item) {
        
        var Permiso = $(this).find('td:eq(0)').html().toString().trim();

        var fecha1 = new Date($(this).find('td:eq(1)').html());
        var FechaE = fecha1.toISOString().slice(0, 10);

        var fecha2 = new Date($(this).find('td:eq(2)').html());
        var FechaS = fecha2.toISOString().slice(0, 10);

        var PesoN = $(this).find('td:eq(3)').html().toString().trim();
        var imp1 = $(this).find('td:eq(4)').html().toString().trim();
        var Expor = $(this).find('td:eq(5)').html().toString().trim();
        var Product = $(this).find('td:eq(6)').html().toString().trim();
        var PesoU = $(this).find('td:eq(7)').html().toString().trim();
        var CanEnv = $(this).find('td:eq(8)').html().toString().trim();
        var Factura = $(this).find('td:eq(9)').html().toString().trim();
        var Obser   = $(this).find('td:eq(10)').html().toString().trim();

        var Ren = $(this).find('td:eq(11)').html().toString();
        var esRen = Ren.toLowerCase();

        var est = $(this).find('td:eq(12)').html().toString();
        var estado = est.toLowerCase(); 
      
        var MedidaU = $(this).find('td:eq(13)').html().toString().trim();
        var EnvTipo = $(this).find('td:eq(14)').html().toString().trim();
        var PuertoE = $(this).find('td:eq(15)').html().toString().trim();
        var PuertoS = $(this).find('td:eq(16)').html().toString().trim();
        var PaisP = $(this).find('td:eq(17)').html().toString().trim();
        var PaisD = $(this).find('td:eq(18)').html().toString().trim();
        var PaisO   = $(this).find('td:eq(19)').html().toString().trim();
        var Almacen = $(this).find('td:eq(20)').html().toString().trim();
        var PermisoR = $(this).find('td:eq(21)').html().toString().trim();
        var TipoPermiso = $(this).find('td:eq(22)').html().toString().trim();
       
        abp.ajax({
            url: abp.appPath + 'api/app/impor-exports',
            type: 'POST',
            abpHandleError: true, //DISABLE AUTO ERROR HANDLING
                                   
            data: JSON.stringify({
               
                NoPermiso: Permiso,
                FechaEmision: FechaE,
                FechaSolicitud: FechaS,
                PesoNeto: parseFloat(PesoN),
                ImportadorId: imp1,
                ExportadorId: Expor,
                ProductoId: Product,
                PesoUnitario: parseFloat(PesoU),
                CantEnvvase: parseFloat(CanEnv),
                NoFactura: Factura,
                Observaciones: Obser,
                EsRenovacion: esRen,  
                Estado: estado,  
                UnidadMedidaId: parseInt(MedidaU),
                TipoEnvaseId: parseInt(EnvTipo),
                PuertoEntradaId: parseInt(PuertoE),
                PuertoSalidaId: parseInt(PuertoS),
                PaisProcedenciaId: parseInt(PaisP),
                PaisDestinoId: parseInt(PaisD),
                PaisOrigenId: parseInt(PaisO),
                AlmacenId: parseInt(Almacen),
                PermisoRenov: PermisoR,
                PermisoDe: TipoPermiso
            
            }),

                success: function (data) {
                $(item).find('td:eq(23)').html('<span class="badge bg-success">Exito</span>');
                $(item).find('td:eq(23)').attr('data-original-title', 'Registro guardado con Éxito!');
            },
            error: function (error) {
                if (error.code == 'Exist') {
                    $(item).find('td:eq(23)').html('<span class="badge bg-warning">Existe</span>');
                    $(item).find('td:eq(23)').attr('data-original-title', error.message);
                }
                else {
                    $(item).find('td:eq(23)').html('<span class="badge bg-danger">Error</span>');
                    $(item).find('td:eq(23)').attr('data - original - title', error.message);
                }
            }
        });
       
    });
    
});