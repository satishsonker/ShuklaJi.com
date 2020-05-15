let $txtUsername, $photo, $txtSource, $btn, $txtFeedback,$feedbackId;

$(document).ready(function () {
    let $dxDataGrid;
    api.get(app.api.urls.web.contactUs.getFeedback).then(function (data) {
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
                caption: "Photo",
                width: 180,
                    cellTemplate: function (container, options) {
                        $(`<div><img src="${options.data.photo}"</div>`).appendTo(container);
                }
            },"username", "source", "feedback"], onToolbarPreparing: function (e) {
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

    $photo = $("#file-uploader").dxFileUploader({
        selectButtonText: "Select photo",
        labelText: "",
        accept: "image/*",
        uploadMode: "useForm",
        onValueChanged: function (e) {
            var files = e.value;
            if (files.length > 0) {
                $("#selected-files .selected-item").remove();
                $.each(files, function (i, file) {
                    var $selectedItem = $("<div />").addClass("selected-item");
                    $selectedItem.append(
                        $("<span />").html("Name: " + file.name + "<br/>"),
                        $("<span />").html("Size " + file.size + " bytes" + "<br/>"),
                    );
                    $selectedItem.appendTo($("#selected-files"));
                    $("#imgLogo").attr("src", URL.createObjectURL(file));
                });
                $("#selected-files").show();
            }
            else
                $("#selected-files").hide();
        }
    }).dxFileUploader("instance");

    $txtUsername = $('#txtUsername').dxTextBox({
        value: "",
        placeholder: "Enter username",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Username is required."
        }]
    }).dxTextBox('instance');

    $txtSource = $('#txtSource').dxTextBox({
        value: "",
        placeholder: "Enter feedback source",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Feedback source is required."
        }]
    }).dxTextBox('instance');

    $txtFeedback = $('#txtFeedback').dxTextArea({
        value: "",
        placeholder: "Enter downtime To time",
        showClearButton: true,
        height: 70
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

    $('#frmAddUserfeedback').on('submit', function (e) {
        var $formData = new FormData();
        $formData.append('file', $photo.option('value')[0]);
        $formData.append('Id', $feedbackId === undefined ? 0 : $feedbackId);
        $formData.append('Source', $txtSource.option('value'));
        $formData.append('Username', $txtUsername.option('value'));
        $formData.append('Feedback', $txtFeedback.option('value'));
        $formData.append('UserId', app.common.userId);
        var $url = $btn.option('text') === 'Update' ? app.api.urls.web.contactUs.updateFeedback : app.api.urls.web.contactUs.saveFeedback;
        api.post($url, $formData).then(function (data) {
            api.successMsgHandler(data);
            app.methods.url.reloadPage(true);
        }).catch(api.errorHandler);
        e.preventDefault();
    });


});
var editDowntime = function ($data) {
    $txtFeedback.option('value', $data.feedback);
    $txtSource.option('value', $data.source);
    $txtUsername.option('value', $data.username);
    $('#frmAddDowntime').data('id', $data.id);
    $btn.option('text', 'Update');
    $feedbackId = $data.id;
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

$(document).on('click', '.form-close', function () {
    $('.model-form').hide();
    $btn.option('text', 'Save');
})