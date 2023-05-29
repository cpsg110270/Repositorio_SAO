$(function () {
    var l = abp.localization.getResource("SAO");
	
	var cuotaImportadorService = window.sAO.cuotaImportadors.cuotaImportadors;
	
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
	    $('#ImportadorFilterLookupOpenButton').on('click', '', function () {
        lastNpDisplayNameId = 'Importador_Filter_NombreImportador';
        lastNpIdId = 'ImportadorIdFilter';
        _lookupModal.open({
            currentId: $('#ImportadorIdFilter').val(),
            currentDisplayName: $('#Importador_Filter_NombreImportador').val(),
            serviceMethod: function () {
                            
                            return window.sAO.cuotaImportadors.cuotaImportadors.getImportadorLookup;
            }
        });
    });
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CuotaImportadors/CreateModal",
        scriptUrl: "/Pages/CuotaImportadors/createModal.js",
        modalClass: "cuotaImportadorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CuotaImportadors/EditModal",
        scriptUrl: "/Pages/CuotaImportadors/editModal.js",
        modalClass: "cuotaImportadorEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            añoMin: $("#AñoFilterMin").val(),
			añoMax: $("#AñoFilterMax").val(),
			cuotaMin: $("#CuotaFilterMin").val(),
			cuotaMax: $("#CuotaFilterMax").val(),
			importadorId: $("#ImportadorIdFilter").val()
        };
    };

    var dataTable = $("#CuotaImportadorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(cuotaImportadorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.CuotaImportadors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.cuotaImportador.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.CuotaImportadors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    cuotaImportadorService.delete(data.record.cuotaImportador.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "cuotaImportador.año" },
			{ data: "cuotaImportador.cuota" },
            {
                data: "importador.nombreImportador",
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

    $("#NewCuotaImportadorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        cuotaImportadorService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/cuota-importadors/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'añoMin', value: input.añoMin },
                            { name: 'añoMax', value: input.añoMax },
                            { name: 'cuotaMin', value: input.cuotaMin },
                            { name: 'cuotaMax', value: input.cuotaMax }, 
                            { name: 'importadorId', value: input.importadorId }
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
