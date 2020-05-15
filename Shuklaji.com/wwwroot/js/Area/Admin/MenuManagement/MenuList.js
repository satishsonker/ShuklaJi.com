/// <reference path="../../../common/apiurls.js" />
$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.menuManagement.getMenu).then(function (data) {
        $dxDataGrid=  $("#gridContainer").dxDataGrid({
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
                allowedPageSizes: [10, 20,50,100],
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
                                    <li title="Edit"><a href="${app.page.urls.adminArea.menuManagement.addMenu}?menuid=${options.data.menuId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteMenu(${options.data.menuId})"><i class="la la-trash"></i></li>
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
                }, "menuName", "displayName", "parentMenu", "menuPath", "iconClass", "iconColor", "displayOrder","position","accessBy"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.menuManagement.addMenu);
                            //location = ;// "/admin/MenuManagement/GetMenu";
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

var deleteMenu = function (menuId) {
    app.methods.page.deleteConfirm(function () {
        menuId = parseInt(menuId);
        if (!isNaN(menuId) && menuId > 0) {
            var param = { menuId: menuId, userId: app.common.userId };
            api.post(app.api.urls.menuManagement.deleteMenu, param).then(function (data) {
                api.successMsgHandler(data);
                app.methods.url.reloadPage(true);
                //location.reload(true);
            }).catch(api.errorHandler);
        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, menuId);
};
