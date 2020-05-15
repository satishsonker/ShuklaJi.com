$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.userManagement.getAstrologer).then(function (data) {
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
                width: 155,
                allowFiltering: false,
                allowSorting: false,
                cellTemplate: function (container, options) {
                    $(`<div class="action-panel">`)
                        .append(`<ul>
                                    <li title="Edit"><a href="${app.page.urls.adminArea.userManagement.addAstrologer}?astrologerid=${options.data.astrologerId}"><i class="la la-pencil"></i></a></li>
                                    <li title="Delete" onclick="deleteUser(${options.data.astrologerId})"><i class="la la-trash"></i></li>
                                    <li title="View Profile" onclick="viewUser(${options.data.astrologerId})"><i class="la la-eye" style="font-size:21px;"></i></li>
                                    <li title="Add/Edit Schedule"><a href="${app.page.urls.adminArea.userManagement.astrologerSchedule}?astrologerId=${options.data.astrologerId}"><i class="far fa-clock"></i></a></li>
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
                }, {
                    caption: "Photo",
                    width: 125,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $(`<img src="${options.data.photo}" style="width:50px;height:50px;">`).appendTo(container);
                    }
                }, "name", "email", "mobile", "experience"], onToolbarPreparing: function (e) {
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
                }, {
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
                        hint: 'Filter data',
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

var deleteUser = function (astrologersId) {
    app.methods.page.deleteConfirm(function () {
        var $userId = parseInt(astrologersId);
        if (!isNaN($userId) && $userId > 0) {
            var $param = {};
            $param.astrologerId = astrologersId;
            $param.userid = app.common.userId;
            api.post(app.api.urls.userManagement.deleteAstrologer, $param).then(function (data) {
                api.successMsgHandler(data);
                location.reload(true);
            }).catch(api.errorHandler);

        }
        else {
            app.toast.warning(app.userMsg.invalidRecord);
        }
    }, astrologersId);
};
var viewUser = function (astrologersId) {
    if (parseInt(astrologersId) > 0) {
        var dd = app.common.userRole;
        app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.viewAstrologers + `?astrologerid=${astrologersId}${(app.common.userRole.toLowerCase().indexOf('admin') > -1
?"&txntype=admin":"")}`);
    }
    else {
        app.toast.warning(app.userMsg.invalidRecord);
    }
};