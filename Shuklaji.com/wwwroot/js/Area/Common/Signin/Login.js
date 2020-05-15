$(document).ready(function () {
    let $appSetting = JSON.parse(app.methods.storage.getData(app.methods.storage.storageKey.appSetting));
    if ($appSetting !== null && $appSetting.length > 0) {
        $('#appLogo').attr('src', $appSetting[0].appLogo);
        $('#appName').text(`Login to ${$appSetting[0].appName}`);
    }
    else {
        app.toast.warning('Unable to get App setting data.');
    }
    let $txtUsername, $txtPassword, $btnLogin;
    $txtUsername = $('#txtUserName').dxTextBox({
        value: "",
        placeholder: "Enter user name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "User name is required."
        }]
        }).dxTextBox('instance');

    $txtPassword = $('#txtPassword').dxTextBox({
        value: "",
        placeholder: "Enter password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtPassword.option("mode", $txtPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Password is required."
        }]
        }).dxTextBox('instance');

    $btnLogin = $("#btnLogin").dxButton({
        icon: "check",
        type: "success",
        text: "Login",
        height: 30,
        width: 180,
        useSubmitBehavior: true
    }).dxButton('instance');

    $('#frmLogin').on('submit', function (e) {
        var param = {};
        param.username = $txtUsername.option('value');
        param.password = $txtPassword.option('value');
        api.post(app.api.urls.signin.login, param).then(function (data) {
            api.successMsgHandler(data);
            if (data.message === app.api.statusMsg.ValidUser) {
                app.common.userName = $txtUsername.option('value');
                app.methods.msg.setMsgQueue(data.message);
                app.methods.storage.setData(app.methods.storage.storageKey.loginData,data.data);
                app.methods.url.redirectTo(app.page.urls.common.dashboard.getDashboard);
            }
        }).then(api.errorHandler);
        e.preventDefault();
    });
});