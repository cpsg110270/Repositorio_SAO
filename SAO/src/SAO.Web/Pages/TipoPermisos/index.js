$(function () {
    var l = abp.localization.getResource("SAO");
	
	var tipoPermisoService = window.sAO.tipoPermisos.tipoPermisos;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TipoPermisos/CreateModal",
        scriptUrl: "/Pages/TipoPermisos/createModal.js",
        modalClass: "tipoPermisoCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TipoPermisos/EditModal",
        scriptUrl: "/Pages/TipoPermisos/editModal.js",
        modalClass: "tipoPermisoEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            codigo: $("#CodigoFilter").val(),
			desripcion: $("#DesripcionFilter").val()
        };
    };

    var dataTable = $("#TipoPermisosTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(tipoPermisoService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.TipoPermisos.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.TipoPermisos.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    tipoPermisoService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "codigo" },
			{ data: "desripcion" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewTipoPermisoButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        tipoPermisoService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/tipo-permisos/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'codigo', value: input.codigo }, 
                            { name: 'desripcion', value: input.desripcion }
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
