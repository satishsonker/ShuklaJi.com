$(document).ready(function () {
    let $dxDataGrid;
    api.post(app.api.urls.roleManagement.getRoles).then(function (data) {
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
                allowedPageSizes: [5, 10, 20],
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
                                    <li title="Edit"><a href="/admin/RoleManagement/AddRole?roleid=${options.data.roleId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteRole(${options.data.roleId})"><i class="la la-trash"></i></li>
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
            }, "roleName", "displayName"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",                    
                    options: {
                        icon: "plus",
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.roleManagement.addRole);
                        }
                    }
                },{
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

var deleteRole = function (roleId) {
    app.methods.page.deleteConfirm(function () {
        roleId = parseInt(roleId);
        if (!isNaN(roleId) && roleId > 0) {
            api.post(app.api.urls.roleManagement.deleteRoles + `?roleid=${roleId}&userId=${app.common.userId}`).then(function (data) {
                api.successMsgHandler(data);
                location.reload(true);
            }).catch(api.errorHandler);
        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, roleId);
};
