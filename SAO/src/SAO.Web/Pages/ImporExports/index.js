$(function () {
    var l = abp.localization.getResource("SAO");
	
	var imporExportService = window.sAO.imporExports.imporExports;
	
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
        viewUrl: abp.appPath + "ImporExports/CreateModal",
        scriptUrl: "/Pages/ImporExports/createModal.js",
        modalClass: "imporExportCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "ImporExports/EditModal",
        scriptUrl: "/Pages/ImporExports/editModal.js",
        modalClass: "imporExportEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            noPermiso: $("#NoPermisoFilter").val(),
			fechaEmisionMin: $("#FechaEmisionFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			fechaEmisionMax: $("#FechaEmisionFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			fechaSolicitudMin: $("#FechaSolicitudFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			fechaSolicitudMax: $("#FechaSolicitudFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			pesoNetoMin: $("#PesoNetoFilterMin").val(),
			pesoNetoMax: $("#PesoNetoFilterMax").val(),
			pesoUnitarioMin: $("#PesoUnitarioFilterMin").val(),
			pesoUnitarioMax: $("#PesoUnitarioFilterMax").val(),
			cantEnvvaseMin: $("#CantEnvvaseFilterMin").val(),
			cantEnvvaseMax: $("#CantEnvvaseFilterMax").val(),
			noFactura: $("#NoFacturaFilter").val(),
			observaciones: $("#ObservacionesFilter").val(),
            esRenovacion: (function () {
                var value = $("#EsRenovacionFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            estado: (function () {
                var value = $("#EstadoFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			importadorId: $("#ImportadorIdFilter").val(),			exportadorId: $("#ExportadorIdFilter").val(),			productoId: $("#ProductoIdFilter").val(),			unidadMedidaId: $("#UnidadMedidaIdFilter").val(),			tipoEnvaseId: $("#TipoEnvaseIdFilter").val(),			puertoEntradaId: $("#PuertoEntradaIdFilter").val(),			puertoSalidaId: $("#PuertoSalidaIdFilter").val(),			paisProcedenciaId: $("#PaisProcedenciaIdFilter").val(),			paisDestinoId: $("#PaisDestinoIdFilter").val(),			paisOrigenId: $("#PaisOrigenIdFilter").val(),			almacenId: $("#AlmacenIdFilter").val(),			permisoRenov: $("#PermisoRenovFilter").val(),			permisoDe: $("#PermisoDeFilter").val()
        };
    };

    var dataTable = $("#ImporExportsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(imporExportService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SAO.ImporExports.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.imporExport.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SAO.ImporExports.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    imporExportService.delete(data.record.imporExport.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "imporExport.noPermiso" },
            {
                data: "imporExport.fechaEmision",
                render: function (fechaEmision) {
                    if (!fechaEmision) {
                        return "";
                    }
                    
					var date = Date.parse(fechaEmision);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },

            {
                data: "tipoPermiso.desripcion",
                defaultContent: ""
            },

            {
                data: "imporExport.fechaSolicitud",
                render: function (fechaSolicitud) {
                    if (!fechaSolicitud) {
                        return "";
                    }
                    
					var date = Date.parse(fechaSolicitud);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "importador.nombreImportador",
                defaultContent: ""
            },
            {
                data: "exportador.nombreExportador",
                defaultContent: ""
            },
            {
                data: "producto.nombreComercia",
                defaultContent: ""
            },
            {
                data: "imporExport.pesoNeto",
                render: function (pesoNeto) {
                    var formato = numberWithCommas(pesoNeto);
                    return '<b>'+ formato +'</b>';
                },
               className: "dt-right"
            },

            {  data: "imporExport.pesoUnitario",
                render: function (pesoUnitario) {
                    var formato = numberWithCommas(pesoUnitario);
                    return formato; },
                 className: "dt-right"
            },

            { data: "imporExport.cantEnvvase" },

            {
                data: "unidadMedida.abreviatura",
                defaultContent: ""
            },
            {
                data: "tipoEnvase.desEnvase",
                defaultContent: ""
            },
                       
            {
                data: "pais.nombrePais",
                defaultContent: ""
            },
            {
                data: "puertoEntradaSalida.nombrePuerto",
                defaultContent: ""
            },
            {
                data: "puertoEntradaSalida1.nombrePuerto",
                defaultContent: ""
            },
            {
                data: "pais1.nombrePais",
                defaultContent: ""
            },

            {
                data: "almacen.nombreAlmacen",
                defaultContent: ""
            },
             {
                data: "pais2.nombrePais",
                defaultContent: ""
            },
            { data: "imporExport.noFactura" },
			{ data: "imporExport.observaciones" },
            {
                data: "imporExport.esRenovacion",
                render: function (esRenovacion) {
                    return esRenovacion ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "imporExport.estado",
                render: function (estado) {
                    return estado ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "imporExport1.noPermiso",
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

    $("#NewImporExportButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        imporExportService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/impor-exports/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'noPermiso', value: input.noPermiso },
                            { name: 'fechaEmisionMin', value: input.fechaEmisionMin },
                            { name: 'fechaEmisionMax', value: input.fechaEmisionMax },
                            { name: 'fechaSolicitudMin', value: input.fechaSolicitudMin },
                            { name: 'fechaSolicitudMax', value: input.fechaSolicitudMax },
                            { name: 'pesoNetoMin', value: input.pesoNetoMin },
                            { name: 'pesoNetoMax', value: input.pesoNetoMax },
                            { name: 'pesoUnitarioMin', value: input.pesoUnitarioMin },
                            { name: 'pesoUnitarioMax', value: input.pesoUnitarioMax },
                            { name: 'cantEnvvaseMin', value: input.cantEnvvaseMin },
                            { name: 'cantEnvvaseMax', value: input.cantEnvvaseMax }, 
                            { name: 'noFactura', value: input.noFactura }, 
                            { name: 'observaciones', value: input.observaciones }, 
                            { name: 'esRenovacion', value: input.esRenovacion }, 
                            { name: 'estado', value: input.estado }, 
                            { name: 'importadorId', value: input.importadorId }
, 
                            { name: 'exportadorId', value: input.exportadorId }
, 
                            { name: 'productoId', value: input.productoId }
, 
                            { name: 'unidadMedidaId', value: input.unidadMedidaId }
, 
                            { name: 'tipoEnvaseId', value: input.tipoEnvaseId }
, 
                            { name: 'puertoEntradaId', value: input.puertoEntradaId }
, 
                            { name: 'puertoSalidaId', value: input.puertoSalidaId }
, 
                            { name: 'paisProcedenciaId', value: input.paisProcedenciaId }
, 
                            { name: 'paisDestinoId', value: input.paisDestinoId }
, 
                            { name: 'paisOrigenId', value: input.paisOrigenId }
, 
                            { name: 'almacenId', value: input.almacenId }
, 
                            { name: 'permisoRenov', value: input.permisoRenov }
, 
                            { name: 'permisoDe', value: input.permisoDe }
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

function numberWithCommas(numero) {
    const numeroFormateado = numero.toFixed(2);
    const partes = numeroFormateado.split(".");
    const parteEntera = partes[0];
    const parteDecimal = partes[1];
    const parteEnteraFormateada = parteEntera.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    const numeroFinal = parteEnteraFormateada + "." + parteDecimal;
    return numeroFinal;

}

