$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    var $txtContactTitle, $txtAddress, $txtMobile, $txtEmail, $ddlConsultationMethods, $txtContactDecription, $txtInstagramLink, $txtTwitterLink, $txtYoutubeLink, $txtFacebookLink, $txtGoogleLink;

    $txtInstagramLink = $('#txtInstagramLink').dxTextBox({
        value: "",
        placeholder: "Enter instagram link.",
        showClearButton: true
    }).dxTextBox('instance');

    $txtGoogleLink = $('#txtGoogleLink').dxTextBox({
        value: "",
        placeholder: "Enter google link.",
        showClearButton: true
    }).dxTextBox('instance');

    $txtFacebookLink = $('#txtFacebookLink').dxTextBox({
        value: "",
        placeholder: "Enter facebook link.",
        showClearButton: true
    }).dxTextBox('instance');

    $txtYoutubeLink = $('#txtYoutubeLink').dxTextBox({
        value: "",
        placeholder: "Enter youtube link.",
        showClearButton: true
    }).dxTextBox('instance');

    $txtTwitterLink = $('#txtTwitterLink').dxTextBox({
        value: "",
        placeholder: "Enter twitter link.",
        showClearButton: true
    }).dxTextBox('instance');

    $txtContactDecription = $(".html-editor").dxHtmlEditor({
        height: 200,
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
            message: "Contact desciption is required."
        }]
    }).dxHtmlEditor('instance');

    api.get(app.api.urls.misc.refLookupList + `?key=consultMethod`).then(function (data) {
        $ddlConsultationMethods = $('#ddlConsultationMethods').dxTagBox({
            placeholder: "Choose Consultation Method",
            showClearButton: true,
            dataSource: new DevExpress.data.ArrayStore({
                data: data.data.filter(value => value.key.toLowerCase() === 'consultmethod'),
                key: "id"
            }),
            displayExpr: "value",
            valueExpr: "value"
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Consultation method is required."
            }]
        }).dxTagBox('instance');
        //api.get(app.api.urls.common.dropdownList + `?key=${app.api.proc.dropdownList.consultMethod}`).then(function (data) {
        //    if (data.data.length > 0) {
        //        $ddlConsultationMethods = $('#ddlConsultationMethods').dxTagBox({
        //            placeholder: "Choose Consultation Method",
        //            showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
        //                data: data.data,
        //                key: "id"
        //            }),
        //            displayExpr: "value",
        //            valueExpr: "value"
        //        }).dxValidator({
        //            validationRules: [{
        //                type: "required",
        //                message: "Consultation method is required."
        //            }]
        //        }).dxTagBox('instance');
        //    }
        //}).catch(api.errorHandler);

        $txtEmail = $('#txtEmail').dxTextBox({
            value: "",
            placeholder: "Enter email address.",
            showClearButton: true
        }).dxValidator({
            validationRules: [{
                type: "email",
                message: "Email address is invalid."
            }, {
                type: "required",
                message: "Email address is required."
            }]
        }).dxTextBox('instance');

        $txtMobile = $('#txtMobile').dxNumberBox({
            value: "",
            placeholder: "Enter mobile number.",
            showClearButton: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Mobile number is required."
            },
            {
                type: "stringLength",
                min: 10,
                max: 13,
                message: "Mobile number is invalid."
            }, {
                type: "pattern",
                pattern: '^[0-9]{10,13}$',
                message: "Mobile number should be in digit only."
            }]
        }).dxNumberBox('instance');

        $txtAddress = $('#txtAddress').dxTextBox({
            value: "",
            placeholder: "Enter address.",
            showClearButton: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Address is required."
            }]
        }).dxTextBox('instance');

        $txtContactTitle = $('#txtContactTitle').dxTextBox({
            value: "",
            placeholder: "Enter contact title.",
            showClearButton: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Contact title is required."
            }]
        }).dxTextBox('instance');



        api.get(app.api.urls.web.contactUs.getContactDetails).then(function (data) {
            if (data.data) {
                $txtContactTitle.option('value', data.data.contactTitle);
                $txtContactDecription.option('value', data.data.contactDescription);
                $txtAddress.option('value', data.data.address);
                $txtMobile.option('value', data.data.mobile);
                $txtEmail.option('value', data.data.email);
                $txtFacebookLink.option('value', data.data.facebookLink);
                $txtGoogleLink.option('value', data.data.googleLink);
                $txtInstagramLink.option('value', data.data.instagramLink);
                $txtTwitterLink.option('value', data.data.twitterLink);
                $txtYoutubeLink.option('value', data.data.youtubeLink);
                $ddlConsultationMethods.option('value', data.data.consultationMethods.split(','));
            }
            else {
                app.toast.warning(app.userMsg.noRecord);
            }
        });

        $("#btnSave").dxButton({
            stylingMode: "contained",
            text: "Save",
            type: "success",
            width: 120,
            useSubmitBehavior: true,
            onClick: function () {
                if ($('.dx-validationsummary').children().length > 0) {
                    $('#summary-container').show();
                }
            }
        });

        $('#frmSaveContactUs').on('submit', function (e) {
            var $param = {};
            $param.ContactTitle = $txtContactTitle.option('value');
            $param.ContactDescription = $txtContactDecription.option('value');
            $param.Address = $txtAddress.option('value');
            $param.Mobile = $txtMobile.option('value');
            $param.Email = $txtEmail.option('value');
            $param.ConsultationMethods = $ddlConsultationMethods.option('value').toString();
            $param.UserId = app.common.userId

            $param.GoogleLink = $txtGoogleLink.option('value');
            $param.FacebookLink = $txtFacebookLink.option('value');
            $param.InstagramLink = $txtInstagramLink.option('value');
            $param.TwitterLink = $txtTwitterLink.option('value');
            $param.YoutubeLink = $txtYoutubeLink.option('value');

            api.post(app.api.urls.web.contactUs.saveContactDetails, $param).then(function (data) {
                api.successMsgHandler(data);
                app.methods.msg.setMsgQueue(data.message);
                app.methods.url.reloadPage(true);
            }).catch(api.errorHandler);

            e.preventDefault();
        });
    });

    var openPixBrowser = function () {
        $('#fileLogo').click();
    };
});
