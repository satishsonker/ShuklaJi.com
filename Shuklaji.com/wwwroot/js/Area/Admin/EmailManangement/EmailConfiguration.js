/// <reference path="../../../common/apiurls.js" />
/// <reference path="../../../common/constants.js" />
/// <reference path="../../../common/global.js" />

$(document).ready(function () {
    var $query = app.methods.url.urlSearchParams();
    var $txtServerName, $txtConfigName, $numPortNumber, $txtUsername, $txtPassword, $btnSave;
    $numPortNumber = $('#numPortNumber').dxNumberBox({
        value: "",
        placeholder: "Enter port number",
        format: '##',
        min: 0,
        max: 9999,
        showSpinButton: true,
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Port number is required."
        }]
    }).dxNumberBox('instance');
    $txtServerName = $('#txtServerName').dxTextBox({
        value: "",
        placeholder: "Enter server name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Server name is required."
        }]
    }).dxTextBox('instance');

    $txtConfigName = $('#txtConfigName').dxTextBox({
        value: "",
        placeholder: "Enter configuration name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Configuration name is required."
        }, {
            type: "async",
            message: "Email Configuration is already exists.",
            validationCallback: function (params) {
                if (!$query.has('configid')) {
                    return app.methods.api.isDataExists(app.api.proc.isDataExist.emailConfig, params.value);
                }
                else {
                    var d = $.Deferred();
                    d.resolve(true);
                    return d;
                }
            }
        }]
    }).dxTextBox('instance');

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
            message: "password is required."
        }]
    }).dxTextBox('instance');

    $btnSave = $("#btnSave").dxButton({
        icon: "check",
        type: "success",
        text: "Save",
        useSubmitBehavior: true
    }).dxButton('instance');

    $('#frmAddEmailConfig').on('submit', function (e) {
        var $param = {};
        var $url = $query.has('configid') ? app.api.urls.emailManagement.updateEmailConfig : app.api.urls.emailManagement.saveEmailConfig;
        $param.ConfigName = $txtConfigName.option('value');
        $param.ServerName = $txtServerName.option('value');
        $param.Port = $numPortNumber.option('value');
        $param.UserName = $txtUsername.option('value');
        $param.Password = $txtPassword.option('value');
        $param.UserId = app.common.userId;
        $param.ConfigId = $query.has('configid') ? $query.get('configid') : 0;
        api.post($url, $param).then(function (data) {
            api.successMsgHandler(data);
            app.methods.msg.setMsgQueue(data.message);
            app.methods.url.reloadPage();
        }).catch(api.errorHandler);
        e.preventDefault();
    });

    if ($query.has('configid')) {
        api.get(app.api.urls.emailManagement.getEmailConfig + `?configid=${$query.get('configid')}`).then(function (data) {
            if (data.data.length > 0) {
                $txtConfigName.option('value', data.data[0].configName);
                $txtPassword.option('value', data.data[0].password);
                $txtServerName.option('value', data.data[0].serverName);
                $txtUsername.option('value', data.data[0].username);
                $numPortNumber.option('value', data.data[0].port);
            }
        }).catch(api.errorHandler);
        $btnSave.option('text', 'Update');
    }
    else {
        $btnSave.option('text', 'Save');
    }
});