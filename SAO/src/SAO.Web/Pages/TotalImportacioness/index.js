$(function () {
    var l = abp.localization.getResource("SAO");
	
	var totalImportacionesService = window.sAO.totalImportacioness.totalImportacioness;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TotalImportacioness/CreateModal",
        scriptUrl: "/Pages/TotalImportacioness/createModal.js",
        modalClass: "totalImportacionesCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TotalImportacioness/EditModal",
        scriptUrl: "/Pages/TotalImportacioness/editModal.js",
        modalClass: "totalImportacionesEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            anioMin: $("#AnioFilterMin").val(),
			anioMax: $("#AnioFilterMax").val(),
			cuotaAsignadaMin: $("#CuotaAsignadaFilterMin").val(),
			cuotaAsignadaMax: $("#CuotaAsignadaFilterMax").val(),
			cuotaConsumidaMin: $("#CuotaConsumidaFilterMin").val(),
			cuotaConsumidaMax: $("#CuotaConsumidaFilterMax").val(),
			importadorId: $("#ImportadorIdFilter").val(),			tipoProductoId: $("#TipoProductoIdFilter").val(),			asraeId: $("#AsraeIdFilter").val()
        };
    };

    var dataTable = $("#TotalImportacionessTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(totalImportacionesService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.TotalImportacioness.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.totalImportaciones.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.TotalImportacioness.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    totalImportacionesService.delete(data.record.totalImportaciones.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "totalImportaciones.anio" },
			{ data: "totalImportaciones.cuotaAsignada" },
			{ data: "totalImportaciones.cuotaConsumida" },
            {
                data: "importador.nombreImportador",
                defaultContent : ""
            },
            {
                data: "tipoProducto.desProducto",
                defaultContent : ""
            },
            {
                data: "asrae.codigo_ASHRAE",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewTotalImportacionesButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        totalImportacionesService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/total-importacioness/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'anioMin', value: input.anioMin },
                            { name: 'anioMax', value: input.anioMax },
                            { name: 'cuotaAsignadaMin', value: input.cuotaAsignadaMin },
                            { name: 'cuotaAsignadaMax', value: input.cuotaAsignadaMax },
                            { name: 'cuotaConsumidaMin', value: input.cuotaConsumidaMin },
                            { name: 'cuotaConsumidaMax', value: input.cuotaConsumidaMax }, 
                            { name: 'importadorId', value: input.importadorId }
, 
                            { name: 'tipoProductoId', value: input.tipoProductoId }
, 
                            { name: 'asraeId', value: input.asraeId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
