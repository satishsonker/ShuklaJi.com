/// <reference path="../../../common/apiurls.js" />
/// <reference path="../../../common/constants.js" />
/// <reference path="../../../common/global.js" />

$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.userManagement.getUser).then(function (data) {
        $dxDataGrid = $("#gridContainer").dxDataGrid({
            dataSource: data.data,
            showBorders: true,
            showRowLines: true,
            scrolling: {
                mode: "standard" // or "virtual" | "infinite"
            },
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
                width: 125,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div class="action-panel">`)
                        .append(`<ul>
                                    <li title="Edit"><a href="/admin/UserManagement/AddUser?userid=${options.data.userId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete" onclick="deleteUser(${options.data.roleId})"><i class="la la-trash"></i></li>
                                    <li title="${(options.data.userId === app.common.userId ? 'You cannot change your user status' : 'Activate/Deactivate User')}"><label class="switch"><input class="attr-isactive" type="checkbox" onclick="activateDeactivateUser(${options.data.userId},${options.data.isActive})"  ${(options.data.isActive ? 'checked="checked"' : '')} ${(options.data.userId === app.common.userId ? 'disabled="disabled"' : '')} ><span class="slider round"></span></label></li>
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
                }, "userName", "firstName", "lastName","email","mobile", "roleName"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "fa fa-user-astronaut",
                        hint: 'Add Astrologer',
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.addAstrologer);
                        }
                    }
                },{
                        location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        hint: 'Add User',
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.addUser);
                        }
                    }
                }, {
                        location: "before",
                        widget: "dxButton",
                        options: {
                            icon: "search",
                            hint: 'Search data',
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
                            hint:'Filter data',
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

var deleteUser = function (userId) {   
    app.methods.page.deleteConfirm(function () {
        userId = parseInt(userId);
        if (!isNaN(userId) && userId > 0) {
            api.post(app.api.urls.userManagement.deleteUser + `?userid=${userId}&modifiedBy=${app.common.userId}`).then(function (data) {
                api.successMsgHandler(data);
                location.reload(true);
            }).catch(api.errorHandler);

        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, userId);
};

var activateDeactivateUser = function (userId,isActive) {
    //let $isChecked = $(this).is(':checked');
    if (app.common.userId !== userId) {
        api.post(app.api.urls.userManagement.activeDeactiveUser + `?modifiedby=${app.common.userId}&userid=${userId}&status=${!isActive}`).then(function (data) {
            api.successMsgHandler(data);
            app.methods.msg.setMsgQueue(data.data.message);
            app.methods.url.reloadPage();
        }).catch(api.errorHandler);
    }
    else {
        app.toast.warning(app.userMsg.cannotDeactivateYourSelf);
    }
};
