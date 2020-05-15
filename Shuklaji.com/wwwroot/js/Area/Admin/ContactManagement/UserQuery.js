var $htmlEditor, $queryData, $htmlEditorUserQueryTrails;
$(document).ready(function () {
    api.get(app.api.urls.web.contactUs.getUserQuery).then(function (data) {
        if (data.data) {
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
                    width: 100,
                    allowFiltering: false,
                    allowSorting: false,
                    cellTemplate: function (container, options) {
                        $(`<div class="action-panel">`)
                            .append(`<ul><li title="View" onclick="viewQueryReplies(${options.data.queryId})"><i class="la la-eye"></i></li>

                                    ${(options.data.status === "Open" ? `<li title="Reply" class="userQueryReplyClick" data-data="${app.methods.common.setDataAttr(options.data)}"><i class="la la-reply-all"></i></li>
                                                                        <li title="Resolve" onclick="resolveQuery(${options.data.queryId})"><i class="la la-thumbs-up"></i></li>` : '')}
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
                    }, "name", "mobile", "email", {
                        caption: "Status",
                        width: 180,                        
                        allowFiltering: true,
                        allowSorting: true,
                        cellTemplate: function (container, options) {
                            $(`<div class="userQueryData" title="${options.data.query}" onclick="showQueryData(this)">`)
                                .text(options.data.query.substring(1, 27)+'...')
                                .appendTo(container);
                        }
                    }, {
                        caption: "Status",
                        width: 120,
                        allowFiltering: true,
                        allowSorting: true,
                        cellTemplate: function (container, options) {
                            $(`<div style="padding: 6px 15px;text-align: center;width: 100%;" class="${(options.data.status === "Open"?"badge badge-danger":"badge badge-success")}">`)
                                .append(`<span>${options.data.status}</span>`)
                                .appendTo(container);
                        }
                    }, "createdDate"], onToolbarPreparing: function (e) {
                    var dataGrid = e.component;

                    e.toolbarOptions.items.unshift({
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
        }
        else {
            app.toast.warning(app.userMsg.noRecord);
        }
    }).catch(api.errorHandler);

    $htmlEditor = $(".html-editor").dxHtmlEditor({
        height: 300,
        toolbar: {
            items: [
                "undo", "redo", "separator",
                {
                    formatName: "size",
                    formatValues: ["8pt", "10pt", "12pt", "14pt", "18pt", "24pt", "36pt"]
                },
                {
                    formatName: "font",
                    formatValues: ["Arial", "Courier New", "Georgia", "Impact", "Lucida Console", "Tahoma", "Times New Roman", "Verdana"]
                },
                "separator", "bold", "italic", "strike", "underline", "separator",
                "alignLeft", "alignCenter", "alignRight", "alignJustify", "separator",
                {
                    formatName: "header",
                    formatValues: [false, 1, 2, 3, 4, 5]
                }, "separator",
                "orderedList", "bulletList", "separator",
                "color", "background", "separator",
                "link", "image", "separator",
                "clear", "codeBlock", "blockquote"
            ]
        },
        mediaResizing: {
            enabled: true
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "User reply is required."
        }]
    }).dxHtmlEditor('instance');

    $btnSaveUserQueryReply = $('#btnSaveUserQueryReply').dxButton({
        icon: 'check',
        text: 'Send',
        type: 'success',
        useSubmitBehavior: true
    }).dxButton('instance');
});
$(document).on('click', '.la-times', function () {
    $('.showQuery p').text('').parent().parent().hide();
    $('.showQuery').hide();
});
$(document).on('click', '.la-times', function () {
    $htmlEditor.option('value', '');
    $('.showQueryReply,.showAllQueryReply').parent().hide();
    $('.showQueryReply,.showAllQueryReply').hide();
});
$(document).on('click', '.userQueryReplyClick', function () {
    $queryData = app.methods.common.getDataAttr($(this).data('data'));   
    $('.showQueryReply').parent().show();     
    $('.showQueryReply').show();   
        $htmlEditor.option('value', `Hi ${$queryData.name},<br/><br/>Thanks & Regard<br/>${app.common.appName} Team`);
});
$(document).on('submit', '#frmUserQuery', function (e) {
    var $param = {};
    $param.UserEmail = [];
    $param.UserEmail.push($queryData.email);
    $param.Reply = $htmlEditor.option('value');
    $param.AppName = app.common.appName;
    $param.QueryId = $queryData.queryId;
    $param.UserId = app.common.userId;
    api.post(app.api.urls.web.contactUs.userQueryReply, $param).then(function (data) {
        api.successMsgHandler(data);
        $('.la-times').click();
        //app.methods.msg.setMsgQueue(data.data.message);
        //app.methods.url.reloadPage();
    }).catch(api.errorHandler);

    e.preventDefault();
});

var showQueryData = function (data) {
    $('.showQuery p').text($(data).attr('title')).parent().parent().show();
    $('.showQuery').show()
}
var resolveQuery = function (queryId) {
    api.post(app.api.urls.web.contactUs.resolveUserQuery + `?queryid=${queryId}`).then(function (data) {
        api.successMsgHandler(data);
        app.methods.msg.setMsgQueue(data.data.message);
        app.methods.url.reloadPage();
    }).catch(api.errorHandler);
}

var viewQueryReplies = function ($queryId) {
    api.get(app.api.urls.web.contactUs.getUserQueryReply + `?queryId=${$queryId}`).then(function (data) {
        $('.showAllQueryReply').parent().show();
        $('.showAllQueryReply').show();
        let $stringBuilder = '';
        for (var i = data.data.table1.length - 1; i >= 0; i--) {
            var $tempData = data.data.table1[i];
            $stringBuilder += `<strong>From:</strong> ${$tempData.sentFrom}<br/>
                               <strong>Sent:</strong> ${$tempData.createdDate}<br/>
                               <strong>To:</strong> ${$tempData.sentTo}<br/>
                               <strong>Subject:</strong> ${$tempData.subject}<br/>
                               ${$tempData.reply}<br/>
                                ______________________________________________________________________________________________________________<br/>`;
        }
        $stringBuilder += `<br/><strong>From:</strong> Web Portal<br/>
                               <strong>Sent:</strong> ${data.data.table[0].createdDate}<br/>
                               <strong>To:</strong> ${data.data.table[0].sentTo}<br/>
                               <strong>Subject:</strong> User Quey<br/>
                               ${data.data.table[0].query}`;


        $htmlEditorUserQueryTrails = $('.User-Query-Trails').dxHtmlEditor({
                height: 300,
                mediaResizing: {
                    enabled: true
                }
        }).dxHtmlEditor('instance');

        $htmlEditorUserQueryTrails.option('value', $stringBuilder)
    }).catch(api.errorHandler);
}


