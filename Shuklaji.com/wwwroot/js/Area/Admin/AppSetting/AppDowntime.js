let $fromTime, $toTime, $message, $btn,$downtimeId;

$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.appSetting.getAppDowntime).then(function (data) {
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
                                    <li title="Edit" data-data="" onclick="editDowntime(${app.methods.common.setDataAttr(options.data)})"><i class="la la-pencil"></i></li>
                                    <li title="Delete" onclick="deleteDownTime(${options.data.id})"><i class="la la-trash"></i></li>
                                </ul>`)
                        .appendTo(container);
                }
            }, {
                caption: "Sr. No.",
                width: 75,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div>`)
                        .append(`<span>${(options.rowIndex + 1)}</span>`)
                        .appendTo(container);
                }
                }, {
                    caption: "From Time",
                    width: 180,
                    cellTemplate: function (container, options) {
                        $(`<div>${app.methods.common.getGridDateTime(options.data.fromTime)}</div>`).appendTo(container);
                    }
                }, {
                    caption: "To Time",
                    width: 180,
                    cellTemplate: function (container, options) {
                        $(`<div>${app.methods.common.getGridDateTime(options.data.toTime)}</div>`).appendTo(container);
                    }
                }, "message", {
                    caption: "Status",
                    width: 75,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $(`<div>`)
                            .append(`<span class="badge ${new Date(options.data.toTime) >= new Date() ? 'badge-success' : 'badge-danger'}">${new Date(options.data.toTime) >= new Date() ? 'Active' : 'Expired'}</span>`)
                            .appendTo(container);
                    }
                }], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        onClick: function (e) {
                            $('.model-form').show();
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


    $fromTime = $('#txtFromTime').dxDateBox({
        value: new Date(), type: "datetime",
        placeholder: "Enter downtime From time",
        showClearButton: true,
        min: new Date(),
        onValueChanged: function (data) {
            $toTime.option('min', new Date(data.value))
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Downtime from time is required."
        }]
    }).dxDateBox('instance');
    $toTime = $('#txtToTime').dxDateBox({
        min: new Date($fromTime.option('value')),
        placeholder: "Enter downtime To time", type: "datetime",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Downtime to time is required."
        }]
    }).dxDateBox('instance');  

    $message = $('#txtMessage').dxTextArea({
        value: "",
        placeholder: "Enter downtime message",
        showClearButton: true,
        height: 90
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Downtime message is required."
        }]
    }).dxTextArea('instance');

    $btn = $("#btnSave").dxButton({
        icon: "save",
        type: "success",
        text: "Save",
        useSubmitBehavior: true,
        onClick: function () {
            if ($('.dx-validationsummary').children().length > 0) {
                $('#summary-container').show();
            }
        }
    }).dxButton('instance');

    
});
var editDowntime = function ($data) {
    $fromTime.option('value', $data.fromTime);
    $toTime.option('value', $data.toTime);
    $message.option('value', $data.message);
    $('#frmAddDowntime').data('id', $data.id);
    $btn.option('text', 'Update');
    $downtimeId = $data.id;
    $('.model-form').show();
}
var deleteDownTime = function ($id) {
    app.methods.page.deleteConfirm(function () {
        $id = parseInt($id);
        if (!isNaN($id) && $id > 0) {
            var param = { id: $id, userId: app.common.userId };
            api.post(app.api.urls.appSetting.deleteAppDowntime, param).then(function (data) {
                api.successMsgHandler(data);
                app.methods.url.reloadPage(true);
                //location.reload(true);
            }).catch(api.errorHandler);
        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, $id);
};
$(document).on('submit', '#frmAddDowntime', function (e) {
    e.preventDefault();
    var param = {};
    param.Id = $downtimeId;
    param.FromTime = $fromTime.option('value');
    param.ToTime = $toTime.option('value');
    param.Message = $message.option('value');
    param.UserId = app.common.userId;
    var $url = $btn.option('text') === 'Update' ? app.api.urls.appSetting.updateAppDowntime : app.api.urls.appSetting.saveAppDowntime;
    api.post($url, param).then(function (data) {
        api.successMsgHandler(data);
        app.methods.url.reloadPage(true);
    }).catch(api.errorHandler);
});

$(document).on('click', '.form-close', function() {
    $('.model-form').hide();
    $btn.option('text', 'Save');
})