 

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

$("#SaveTiposCambiosButton").click(function () {

    $.each($("#tableTiposCambios tbody tr"), function (i, item) {
        var fecha = $(this).find('td:eq(0)').html().toString().trim();
        var valor = $(this).find('td:eq(1)').html().toString().trim();
        abp.ajax({
            url: '/api/app/tipoCambio',
            type: 'POST',
            abpHandleError: false, //DISABLE AUTO ERROR HANDLING
            data: JSON.stringify({
                fecha: fecha,
                valor: valor
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