$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    var $numResetLinkValidity, $txtDefaultPassword, $txtAppName;
    $txtAppName = $('#txtAppName').dxTextBox({
        value: "",
        placeholder: "Enter application name.",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Application name is required."
        }]
    }).dxTextBox('instance');

    $txtDefaultPassword = $('#txtDefaultPassword').dxTextBox({
        value: "",
        placeholder: "Enter default password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtDefaultPassword.option("mode", $txtDefaultPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Default password is required"
        }, {
            type: "stringLength",
            min: 6,
            message: "Default password should have at least 6 symbles"
        }]
    }).dxTextBox('instance');

    $numResetLinkValidity = $('#numResetLinkValidity').dxNumberBox({
        value: "",
        placeholder: "Enter Reset Link Validity",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Reset link validity is required."
        }]
    }).dxNumberBox('instance');

    api.get(app.api.urls.appSetting.getAppSetting).then(function (data) {
        if (data.data.length > 0) {
            $("#imgLogo").attr("src", data.data[0].appLogo);
            $txtAppName.option('value', data.data[0].appName);
            $txtDefaultPassword.option('value', data.data[0].defaultPassword);
            $numResetLinkValidity.option('value', data.data[0].resetLinkValidity === null ? 30 : data.data[0].resetLinkValidity);
        }
        else {
            app.toast.warning(app.userMsg.noRecord);
        }
    });

    $("#fileLogo").on("change", function () {

        $("#imgLogo").attr("src", URL.createObjectURL(this.files[0]));
    });

    $(".removeLogo").on("click", function () {

        $("#imgLogo").attr("src", '');
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

    $('#frmAddAppSetting').on('submit', function (e) {
        let $form = $('#frmAddAppSetting');
        var $formData = new FormData();
        let $inputFile = $("#fileLogo");
        $formData.append('file', $inputFile[0].files[0]);
        $formData.append('appname', $txtAppName.option('value'));
        $formData.append('defaultpassword', $txtDefaultPassword.option('value'));
        $formData.append('image', $('#imgLogo').attr('src'));
        $formData.append('userid', app.common.userId);
        $formData.append('resetlinkvalidity', $numResetLinkValidity.option('value'));
        api.postWithFile(app.api.urls.appSetting.setAppSetting, $formData).then(function (data) {
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

