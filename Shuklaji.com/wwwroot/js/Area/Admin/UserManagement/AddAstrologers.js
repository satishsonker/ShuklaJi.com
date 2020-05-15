$(document).ready(function () {
    var $photo, $biography, $query, $txtName, $btn, $txtEmail, $txtMobile, $ddlLanguage, $numExperience, $txtLocation, $txtAddress, $ddlExperties, $txtDob, $ddlConsultOn, $numConsultPrice;
    $query = app.methods.url.urlSearchParams();

    $photo= $("#file-uploader").dxFileUploader({
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
                        $("<span />").html("Type " + file.type + "<br/>"),
                        $("<span />").html("Last Modified Date: " + file.lastModifiedDate)
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
   
    $txtAddress = $('#txtAddress').dxTextArea({
        value: '',
        height: 90,
        placeholder: "Enter astrologer Address",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer address is required."
        }]
    }).dxTextArea("instance");

    $txtLocation = $('#txtLocation').dxTextBox({
        value: "",
        placeholder: "Enter astrologer location",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer location is required."
        }]
    }).dxTextBox('instance');

    $numExperience = $('#numExperience').dxNumberBox({
        value: 0,
        showSpinButtons: true,
        showClearButton: true,
        min: 1,
        max: 70
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Experience is required."
        }]
    }).dxNumberBox('instance');

    $txtEmail = $('#txtEmail').dxTextBox({
        value: "",
        placeholder: "Enter astrologer email",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer email is required."
        }, {
            type: "email",
            message: "Invalid email."
        }]
    }).dxTextBox('instance');

    $txtMobile = $('#txtMobile').dxTextBox({
        value: "",
        placeholder: "Enter astrologer mobile",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer mobile is required."
        }]
    }).dxTextBox('instance');

    $txtName = $('#txtName').dxTextBox({
        value: "",
        placeholder: "Enter astrologer name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer name is required."
        }]
    }).dxTextBox('instance');

    $txtDob = $('#txtDob').dxDateBox({
        displayFormat: "EEEE, MMM dd",
        value: new Date(),
        max: new Date(),
        placeholder: "Enter astrologer DOB",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Astrologer DOB is required."
        }]
    }).dxDateBox('instance');

    $numConsultPrice = $('#numConsultPrice').dxNumberBox({
        value: 0,
        showSpinButtons: true,
        showClearButton: true,
        min: 1
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "ConsultPrice is required."
        }]
    }).dxNumberBox('instance');

    

    $biography = $('.html-editor').dxHtmlEditor({
        height: 400,
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
            message: "Biography is required."
        }]
    }).dxHtmlEditor('instance');

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

    if ($query.has('astrologerid')) {
        $btn.option('text', 'Update');
    } else {
        $btn.option('text', 'Save')
    }

    api.get(app.api.urls.misc.refLookupList + `?key=language,Experties,consultMethod`).then(function (data) {
        $ddlLanguage = $('#ddlLanguage').dxTagBox({
            placeholder: "Choose Language",
            showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                data: data.data.filter(value => value.key.toLowerCase() === 'language'),
                key: "id"
            }),
            displayExpr: "value",
            valueExpr: "value"
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Language is required."
            }]
        }).dxTagBox('instance');

        $ddlExperties = $('#ddlExperties').dxTagBox({
            placeholder: "Choose Experties",
            showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                data: data.data.filter(value => value.key.toLowerCase() === 'experties'),
                key: "id"
            }),
            displayExpr: "value",
            valueExpr: "value"
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Experties is required."
            }]
        }).dxTagBox('instance');

        $ddlConsultOn = $('#ddlConsultOn').dxTagBox({
            placeholder: "Choose Consult On",
            showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                data: data.data.filter(value => value.key.toLowerCase() === 'consultmethod'),
                key: "id"
            }),
            displayExpr: "value",
            valueExpr: "value"
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Consult On is required."
            }]
        }).dxTagBox('instance');
        if ($query.has('astrologerid')) {
            api.get(app.api.urls.userManagement.getAstrologer + `?astrologerid=${parseInt($query.get('astrologerid'))}`).then(function (data) {
                $biography.option('value', data.data[0].biography);
                $txtName.option('value', data.data[0].name);
                $txtEmail.option('value', data.data[0].email);
                $txtMobile.option('value', data.data[0].mobile);
                $ddlLanguage.option('value', data.data[0].language.split(','))
                $numExperience.option('value', data.data[0].experience);
                $txtLocation.option('value', data.data[0].location);
                $txtAddress.option('value', data.data[0].address);
                $ddlExperties.option('value', data.data[0].experties.split(','))
                $txtDob.option('value', data.data[0].dob);// app.methods.common.getSqlDate($txtDob.option('value'));
                $ddlConsultOn.option('value', data.data[0].consultOn.split(','))
                $numConsultPrice.option('value', data.data[0].consultPrice);
                $("#imgLogo").attr("src", data.data[0].photo);
            });
        }
    });

    $(document).on('submit', '#frmAddAstrologers', function (e) {
        var $formData = new FormData();
        $formData.append('Biography', $biography.option('value'));
        $formData.append('file', $photo.option('value')[0]);
        $formData.append('Name',$txtName.option('value'));
        $formData.append('Email',$txtEmail.option('value'));
        $formData.append('Mobile',$txtMobile.option('value'));
        $formData.append('Language',$ddlLanguage.option('value').toString());
        $formData.append('Experience',$numExperience.option('value'));
        $formData.append('Location',$txtLocation.option('value'));
        $formData.append('Address',$txtAddress.option('value'));
        $formData.append('Experties',$ddlExperties.option('value').toString());
        $formData.append('Dob', app.methods.common.getSqlDate($txtDob.option('value')));
        $formData.append('ConsultOn',$ddlConsultOn.option('value').toString());
        $formData.append('ConsultPrice',$numConsultPrice.option('value'));
        $formData.append('AstrologerId',$query.has('astrologerid') ? parseInt($query.get('astrologerid')) : 0);
        $formData.append('UserId',app.common.userId);
        var $url = $btn.option('text') === 'Update' ? app.api.urls.userManagement.updateAstrologer : app.api.urls.userManagement.saveAstrologer;
        api.postWithFile($url, $formData).then(function (data) {
            api.successMsgHandler(data);
            app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.astrologers);
        }).catch(api.errorHandler);

        e.preventDefault();
    });
});

