var $txtKey, $txtValue, $txtRemark, $btn,$id;

$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.misc.refLookupList).then(function (data) {
        $dxDataGrid = $("#gridContainer").dxDataGrid({
            dataSource: data.data,
            showBorders: true,
            showRowLines: true,
            allowSearch: true,
            filterRow: { visible: false },
            headerFilter: { visible: false },
            paging: {
                pageSize: 10
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [10, 20, 50, 100],
                showInfo: true
            },
            columns: [{
                caption: "Action",
                width: 75,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div class="action-panel">`)
                        .append(`<ul>
                                    <li title="Edit" onclick="$openModel(${options.data.id},'${options.data.key}','${options.data.value}','${options.data.remark}')"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteRefLookup(${options.data.id})"><i class="la la-trash"></i></li>
                                </ul>`)
                        .appendTo(container);
                }
            }, {
                caption: "Sr. No.",
                width: 50,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div>`)
                        .append(`<span>${(options.rowIndex + 1)}</span>`)
                        .appendTo(container);
                }
            }, "key", "value", "remark"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        onClick: function (e) {
                            $openModel();
                        }
                    }
                }, {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "search",
                        onClick: function (e) {
                            var $filterRow = $dxDataGrid.option('filterRow');
                            $dxDataGrid.option('filterRow', { visible: !$filterRow.visible });
                        }
                    }
                }, {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "filter",
                        onClick: function (e) {
                            var $headerFilter = $dxDataGrid.option('headerFilter');
                            $dxDataGrid.option('headerFilter', { visible: !$headerFilter.visible });
                        }
                    }
                });
            }
        }).dxDataGrid('instance');
    }).catch(api.errorHandler);

});

var deleteRefLookup = function ($id) {
    app.methods.page.deleteConfirm(function () {
        var $param = {};
        $param.UserId = app.common.userId;
        $param.Id = $id;
        api.post(app.api.urls.misc.deleteReferenceLookup, $param).catch(api.errorHandler).then(function (data) {
            api.successMsgHandler(data);
            $('.model-form').hide();
            app.methods.url.reloadPage();
        });
        e.preventDefault();
    });
        };
$(document).on('click', '.fas.fa-times', function () {
    $('.model-form').hide();
})

var $openModel = function ($refId, $key, $value, $remark) {
    $id = $refId;
    $('.model-form').show();
    $txtKey = $('#txtKey').dxTextBox({
        value: "",
        placeholder: "Enter Reference Key.",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Reference key is required."
        }]
    }).dxTextBox('instance');

    $txtValue = $('#txtValue').dxTextBox({
        value: "",
        placeholder: "Enter Reference Value.",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Reference kevaluey is required."
        }]
    }).dxTextBox('instance');

    $txtRemark = $('#txtRemark').dxTextBox({
        value: "",
        placeholder: "Enter Reference Remark.",
        showClearButton: true
    }).dxTextBox('instance');

    $btn= $("#btnSave").dxButton({
        stylingMode: "contained",
        text: "Save",
        type: "success",
        width: 120,
        useSubmitBehavior: true,
        onClick: function () {
            if ($('.dx-validationsummary').children().length > 0) {
                $('#summary-container').show();
            }
        }
    }).dxButton('instance');
    if ($refId === undefined) {
        $txtKey.option('value', '');
        $txtRemark.option('value', '');
        $txtValue.option('value', '');
        $btn.option('text', 'Save');
    }
    else {
        $txtKey.option('value', $key);
        $txtRemark.option('value', $remark === null || $remark === 'null'? '' : $remark);
        $txtValue.option('value', $value);
        $btn.option('text', 'Update');
    }
}

$(document).on('submit', '#frmAddRef', function (e) {
    var $param = {}, $url;
    $url = $btn.option('text') === 'Save' ? app.api.urls.misc.saveReferenceLookup : app.api.urls.misc.updateReferenceLookup;
    $param.Key = $txtKey.option('value');
    $param.Remark = $txtRemark.option('value');
    $param.Value = $txtValue.option('value');
    $param.UserId = app.common.userId;
    $param.Id = $id;
    api.post($url, $param).catch(api.errorHandler).then(function (data) {
        api.successMsgHandler(data);
        $('.model-form').hide();
        app.methods.url.reloadPage();
    });
    e.preventDefault();
});