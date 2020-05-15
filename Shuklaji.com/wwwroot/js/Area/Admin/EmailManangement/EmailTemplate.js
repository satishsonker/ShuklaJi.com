
$(document).ready(function() {
    var $ddlEmailTemplate, $txtSubject, $txtEmailTemplate, $txtConfigName;

    api.get(app.api.urls.common.dropdownList + `?key=${app.api.proc.dropdownList.emailTempate}`).then(function (data) {
        //if (data.data.length > 0) {
            $ddlEmailTemplate = $('#ddlTemplateName').dxSelectBox({
                showClearButton: true,
                dataSource: new DevExpress.data.ArrayStore({
                    data: data.data,
                    key: "value",
                    value: data.data[0].value
                }),
                displayExpr: "value",
                valueExpr: "id",
                onValueChanged: function (data) {
                    api.get(app.api.urls.emailManagement.getEmailTemplate + `?templateId=${data.value}`).then(function (data) {
                        $txtSubject.option('value', data.data.subject);
                        $txtEmailTemplate.option('value', data.data.emailTemplate);
                    });
                },
                //fieldTemplate: function (value) {
                //    var $container = $("<div>");
                //    var $input = $("<div>").dxTextBox({
                //        text: value === null?'': value.value
                //    });
                //    var $customButton = $('<div class="rgt-0">').dxButton({
                //        icon: "plus",
                //        hint: 'Add new configuration name',
                //        onClick: function (args) {
                //            showPopup();
                //            e.preventDefault();
                //            e.stopPropagation();
                //        }
                //    }).css({
                //        position: "absolute",
                //        right: "0px",
                //        top: "0"
                //    });
                //    $container.append($input).append($customButton);
                //    return $container;
                //}
            }).dxValidator({
                validationRules: [{
                    type: "required",
                    message: "Configuration Name is required."
                }]
            }).dxSelectBox('instance');
        //}
    }).catch(api.errorHandler);

    $txtSubject = $('#txtSubject').dxTextBox({
        value: "",
        placeholder: "Enter email subject",
        showClearButton: true,
        maxLength:500
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Email subject is required."
        }]
        }).dxTextBox('instance');

    

    $btnAddConfigName = $('#btnAddConfigName').dxButton({
        icon: 'check',
        text: 'Add',
        type: 'success',
        useSubmitBehavior: true
    });

    $btnSaveTemplate = $('#btnSaveTemplate').dxButton({
        icon: 'check',
        text: 'Add',
        type: 'success',
        useSubmitBehavior: true
    }).dxButton('instance');

    $txtEmailTemplate= $(".html-editor").dxHtmlEditor({
        height:200,
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
            message: "Email Template is required."
        }]
    }).dxHtmlEditor('instance');

    var showPopup = function () {
        $('#txtConfigNameContainer').append('<div id="txtConfigName"></div>');
        $txtConfigName = $('#txtConfigName').dxTextBox({
            value: "",
            placeholder: "Enter template config name",
            showClearButton: true,
            maxLength: 500
        }).dxTextBox('instance');
        $('.popup-wrapper').show(200);
    };

    $('#frmAddEmailTemplate').on('submit', function () {
        var param = {};
        param.UserId = app.common.userId;
        param.EmailTemplate = $txtEmailTemplate.option('value');
        param.EmailTemplateName = $ddlEmailTemplate.option('value');
        param.Subject = $txtSubject.option('value');
        api.post(app.api.urls.emailManagement.saveEmailTemplate, param).then(function (data) {
            api.successMsgHandler(data);
        }).catch(api.errorHandler);
    });
});


var hidePopup = function () {
    $('#txtConfigName').remove();
    $('.popup-wrapper').hide(200);
};

var saveEmailTemplateName = function() {

}