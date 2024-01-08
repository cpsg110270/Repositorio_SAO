$(function () {
    var l = abp.localization.getResource("SAO");
	
	var productoService = window.sAO.productos.productos;
	
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
        viewUrl: abp.appPath + "Productos/CreateModal",
        scriptUrl: "/Pages/Productos/createModal.js",
        modalClass: "productoCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Productos/EditModal",
        scriptUrl: "/Pages/Productos/editModal.js",
        modalClass: "productoEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            noProducto: $("#NoProductoFilter").val(),
            nombreComercia: $("#NombreComerciaFilter").val(),
			uso: $("#UsoFilter").val(),
			fabricanteId: $("#FabricanteIdFilter").val(),			asraeId: $("#AsraeIdFilter").val(),			tipoProductoId: $("#TipoProductoIdFilter").val(),			sustanciaElementalId: $("#SustanciaElementalFilter").val()
        };
    };

    var dataTable = $("#ProductosTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(productoService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.Productos.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.producto.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.Productos.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    productoService.delete(data.record.producto.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "producto.noProducto" },
			{ data: "producto.nombreComercia" },
			{ data: "producto.uso" },
            {
                data: "fabricante.nombreFabricante",
                defaultContent : ""
            },
            {
                data: "asrae.codigo_ASHRAE",
                defaultContent : ""
            },
            {
                data: "tipoProducto.desProducto",
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

    $("#NewProductoButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        productoService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/productos/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'nombreComercia', value: input.nombreComercia }, 
                            { name: 'uso', value: input.uso }, 
                            { name: 'fabricanteId', value: input.fabricanteId }
, 
                            { name: 'asraeId', value: input.asraeId }
, 
                            { name: 'tipoProductoId', value: input.tipoProductoId }
, 
                            { name: 'sustanciaElementalId', value: input.sustanciaElementalId }
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
    
                $('#SustanciaElementalFilter').select2({
                ajax: {
                    url: abp.appPath + 'api/app/productos/sustancia-elemental-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });
                        mappedItems.unshift({ id: "", text: ' - ' });

                        return { results: mappedItems };
                    }
                }
            });
        
});
