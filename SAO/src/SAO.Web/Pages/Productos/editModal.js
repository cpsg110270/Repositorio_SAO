var abp = abp || {};

abp.modals.productoEdit = function () {
    var initModal = function (publicApi, args) {
        var l = abp.localization.getResource("SAO");
        
        
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
        
        
        
        publicApi.onOpen(function () {
            $('#SustanciaElementalLookup').select2({
                dropdownParent: $('#ProductoEditModal'),
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

                        return { results: mappedItems };
                    }
                }
            });
        });

        var getNewSustanciaElementalIndex = function () {
            var idTds = $($(document).find("#SustanciaElementalTableRows")).find('td[name="id"]');

            if (idTds.length === 0){
                return 0;
            }

            return parseInt($(idTds[idTds.length -1]).attr("index")) +1;
        };

        var getSustanciaElementalIds = function () {
            var ids = [];
            var idTds = $("#SustanciaElementalTableRows td[name='id']");

            for(var i = 0; i< idTds.length; i++){
                ids.push(idTds[i].innerHTML.trim())
            }

            return ids;
        };

        $('#AddSustanciaElementalButton').on('click', '', function(){
            var $select = $('#SustanciaElementalLookup');
            var id = $select.val();
            var existingIds = getSustanciaElementalIds();
            if (!id || id === ''){
                return;
            }
            
            if (existingIds.indexOf(id) >= 0){
                abp.message.warn(l("ItemAlreadyAdded"))
                return;
            }

            $("#SustanciaElementalTable").show();

            var displayName = $select.find('option').filter(':selected')[0].innerHTML;

            var newIndex = getNewSustanciaElementalIndex();

            $("#SustanciaElementalTableRows").append(
                '                                <tr style="text-align: center; vertical-align: middle;" index="'+newIndex+'">\n' +
                '                                    <td style="display: none" name="id" index="'+newIndex+'">'+id+'</td>\n' +
                '                                    <td style="display: none">' +
                '                                        <input value="'+id+'" id="SelectedSustanciaElementalIds['+newIndex+']" name="SelectedSustanciaElementalIds['+newIndex+']"/>\n' +
                '                                    </td>\n' +
                '                                    <td style="text-align: left">'+displayName+'</td>\n' +
                '                                    <td style="text-align: right">\n' +
                '                                        <button class="btn btn-danger btn-sm text-light sustanciaElementalDeleteButton" index="'+newIndex+'"> <i class="fa fa-trash"></i> </button>\n' +
                '                                    </td>\n' +
                '                                </tr>'
            );
        });

        $(document).on('click', '.sustanciaElementalDeleteButton', function (e) {
            e.preventDefault();
            var index = $(this).attr("index");
            $("#SustanciaElementalTableRows").find('tr[index='+index+']').remove();

            setTimeout(
                function()
                {
                    var rows = $(document).find("#SustanciaElementalTableRows").find("tr");

                    if (rows.length === 0){
                        $("#SustanciaElementalTable").hide();
                    }

                    for (var i=0; i<rows.length; i++){
                        $(rows[i]).attr('index', i);
                        $(rows[i]).find('th[scope="Row"]').empty();
                        $(rows[i]).find('th[scope="Row"]').append(i+1);
                        $($(rows[i]).find('td[name="id"]')).attr('index', i);
                        $($(rows[i]).find('input')).attr('id', 'SelectedSustanciaElementalIds['+i+']');
                        $($(rows[i]).find('input')).attr('name', 'SelectedSustanciaElementalIds['+i+']');
                        $($(rows[i]).find('button')).attr('index', i);
                    }
                }, 200);
        });
    };

    return {
        initModal: initModal
    };
};
