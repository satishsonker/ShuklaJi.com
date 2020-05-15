$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.zodiacManagement.getZodiacReport).then(function (data) {
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
                                    <li title="Edit"><a href="/admin/ZodiacManagement/SaveZodiacReport?reportid=${options.data.zodiacReportId}"><i class="la la-pencil"></a></i></li>
                                    <li title="Delete"><a href="/admin/UserManagement/AddUser?userid=${options.data.Id}"><i class="la la-trash"></a></i></li>
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
                }, "zodiacNameEng", "zodiacNameHindi", {
                    dataField: "rangeFrom",
                    caption: "Visible From",
                    dataType: "date",
                    format: 'longDateLongTime',
                    alignment: "center",
                }, {
                    dataField: "rangeTo",
                    caption: "Visible To",
                    dataType: "date",
                    format: 'longDateLongTime',
                    alignment: "center",
                }, {
                    caption: "Description",
                    width: 270,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $(`<div class="rptDesc" title="Click here to see more" data-data="${app.methods.common.encodeHtml(options.data.description)}">`).html(options.data.description.substr(0,50)).appendTo(container);
                    }
                },"reportType"], onToolbarPreparing: function (e) {
                var dataGrid = e.component;

                e.toolbarOptions.items.unshift({
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "plus",
                        hint: 'Add User',
                        onClick: function (e) {
                            app.methods.url.redirectTo(app.page.urls.adminArea.zodiacManagement.saveZodiacReport);
                        }
                    }
                },{
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

$(document).on('click', '.rptDesc', function () {
    $('.showDescription').show();
    $('.showDescription').parent().show();

    var $txtDescription = $('.html-editor-show').dxHtmlEditor({
        height: 200,
        readOnly: true,
        mediaResizing: {
            enabled: true
        }
    }).dxHtmlEditor('instance');

    $txtDescription.option('value', $(this).data('data'));
});

$(document).on('click', '.showDescriptionClose', function () {
    $('.showDescription').hide();
    $('.showDescription').parent().hide();

   
});