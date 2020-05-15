$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.emailManagement.getEmailConfig).then(function (data) {
        $dxDataGrid = $("#gridContainer").dxDataGrid({
            dataSource: data.data,
            showBorders: true,
            showRowLines: true,
            filterRow: { visible: false },
            headerFilter: { visible: false },
            paging: {
                pageSize: 10
            },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [10, 20, 50,100],
                showInfo: true
            },
            columns: [{
                caption: "Action",
                width: 125,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div class="action-panel">`)
                        .append(`<ul>
                                    <li title="Edit"><a href="/admin/EmailManagement/GetConfiguration?configid=${options.data.configId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteEmailConfig(${options.data.configId})"><i class="la la-trash"></i></li>
                                    <li title="Set default configuraton"><label class="switch"><input class="attr-isactive" type="checkbox" onclick="setDefaultConfig(${options.data.configId},${options.data.defaultConfig})"  ${(options.data.defaultConfig ? 'checked="checked"' : '')}><span class="slider round"></span></label></li>
                                </ul>`)
                        .appendTo(container);
                }
            }, {
                caption: "Sr. No.",
                width: 70,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div>`)
                        .append(`<span>${(options.rowIndex + 1)}</span>`)
                        .appendTo(container);
                }
                }, "configName", "serverName", "username"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.emailManagement.getConfiguration);
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
    }).catch(function (erro) {

    });

});

var deleteEmailConfig = function (configId) {
    app.methods.page.deleteConfirm(function () {
        configId = parseInt(configId);
        if (!isNaN(configId) && configId > 0) {
            api.post(app.api.urls.emailManagement.deleteEmailConfig + `?configId=${configId}&userId=${app.common.userId}`).then(function (data) {
                api.successMsgHandler(data);
                app.methods.msg.setMsgQueue(data.message);
                location.reload(true);
            }).catch(api.errorHandler);
        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, configId);
};

var setDefaultConfig = function (configId, defaultConfig) {
        api.post(app.api.urls.emailManagement.setDefaultEmailConfig + `?userId=${app.common.userId}&configId=${configId}&status=${!defaultConfig}`).then(function (data) {
            api.successMsgHandler(data);
            app.methods.msg.setMsgQueue(data.data.message);
            app.methods.url.reloadPage();
        }).catch(api.errorHandler);
};