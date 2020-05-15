$(document).ready(function () {
    var $txtNewPassword, $txtConfirmPassword, $btnChange, $query,$code;
    $query = app.methods.url.urlSearchParams();

    $txtNewPassword = $('#txtNewPassword').dxTextBox({
        value: "",
        placeholder: "Enter new password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtNewPassword.option("mode", $txtNewPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "New password is required."
        }, {
            type: "stringLength",
            min: 6,
            message: "New Password must have at least 6 symbols"
        }]
    }).dxTextBox('instance');

    $txtConfirmPassword = $('#txtConfirmPassword').dxTextBox({
        value: "",
        placeholder: "Enter confirm password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtConfirmPassword.option("mode", $txtConfirnPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "compare",
            comparisonTarget: function () {
                if ($txtNewPassword) {
                    return $txtNewPassword.option("value");
                }
            },
            message: "'New Password' and 'Confirm Password' should be same."
        },
        {
            type: "required",
            message: "Confirm password is required."
        },
        {
            type: "stringLength",
            min: 6,
            message: "Confirm Password must have at least 6 symbols"
        }]
    }).dxTextBox('instance');


    $btnChange = $("#btnChange").dxButton({
        icon: "refresh",
        type: "success",
        text: "Change",
        height: 40,
        width: 100,
        useSubmitBehavior: true,
        template: function (data, container) {
            $("<div class='button-indicator'></div><span class='dx-button-text'>" + data.text + "</span>").appendTo(container);
            buttonIndicator = container.find(".button-indicator").dxLoadIndicator({
                visible: false
            }).dxLoadIndicator("instance");

        }
    }).dxButton('instance');

    $('#frmNewPassword').on('submit', function (e) {
        api.post(app.api.urls.signin.setNewPassword + `?code=${$code}&password=${$txtConfirmPassword.option('value')}`).then(function (data) {
            if (data.message === app.api.statusMsg.PasswordChange) {
                app.methods.msg.setMsgQueue(data.message);
                app.methods.url.redirectTo(app.page.urls.common.siginin.login);
            }
            else {
                api.successMsgHandler(data);
            }
        }).catch(api.errorHandler);
        e.preventDefault();
    });

    if ($query.has('txn')) {
        $code = $query.get('txn');
        app.methods.api.isDataExists(app.api.proc.isDataExist.resetCode, $code).then(function (data) {
            if (!data) {

            }
            else {
                app.methods.msg.setMsgQueue(app.api.statusMsg.InvalidResetLink);
                app.methods.url.redirectTo(app.page.urls.common.siginin.login);
            }
        });
    }
    else {
        app.methods.url.redirectTo(app.page.urls.common.siginin.login);
    }
}); 