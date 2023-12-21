 

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
    alert("EN GUADAR");

    $("#tableLicencias > tbody > tr").each(function (i, tr) {
    /*$.each($("#tableLicencias tbody tr"), function (i, item) {*/
        var Permiso = $(this).find('td:eq(0)').html().toString().trim();
        alert(Permiso);
        var FechaE  = $(this).find('td:eq(1)').html().toString().trim();
        var FechaS  = $(this).find('td:eq(2)').html().toString().trim();
        var PesoN   = $(this).find('td:eq(3)').html().toString().trim();
        var PesoU   = $(this).find('td:eq(4)').html().toString().trim();
        var CanEnv  = $(this).find('td:eq(5)').html().toString().trim();
        var Factura = $(this).find('td:eq(6)').html().toString().trim();
       

        abp.ajax({
            url: '/api/app/ImporExports',
            type: 'POST',
            abpHandleError: false, //DISABLE AUTO ERROR HANDLING
            data: JSON.stringify({
                NoPemiso: Permiso,
                FechaEmision: FechaE,
                FechaSolicitud: FechaS,
                PesoNeto: PesoN,
                PesoUnitario: PesoU,
                CantEnnvase: CanEnv,
                NoFactura:Factura
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