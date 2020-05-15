$(document).ready(function () {
    var $txtUsername, $btnReset;
    $txtUsername = $('#txtUsername').dxTextBox({
        value: "",
        placeholder: "Enter username",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Username is required."
        }, {
                type: "async",
                message: "Username not exists.",
                validationCallback: function (params) {
                    var $d = app.methods.api.isDataExists(app.api.proc.isDataExist.userName, params.value);
                    var d = $.Deferred();
                    $d.then(function (data) {
                        d.resolve(!data);
                    });
                    return d;
                }
            }]
    }).dxTextBox('instance');

    $btnReset = $("#btnReset").dxButton({
        icon: "refresh",
        type: "success",
        text: "Reset",
        height: 30,
        width: 100,
        useSubmitBehavior: true
    }).dxButton('instance');

    $('#frmForgetPassword').on('submit', function (e) {
        var param = {};
        param.username = $txtUsername.option('value');
        api.post(app.api.urls.signin.resetCode + `?username=${param.username}&appName=${app.common.appName}`).then(function (data) {
            api.successMsgHandler(data);
            if (data.message === app.api.statusMsg.ResetCode) {
                app.methods.msg.setMsgQueue(data.message);
                app.methods.storage.setData(app.methods.storage.storageKey.loginData, data.data);
                app.methods.url.redirectTo(app.page.urls.common.siginin.login);
            }
        }).then(api.errorHandler);
        e.preventDefault();
    });
});