$(document).ready(function () {
    var $txtOldPassword, $txtNewPassword, $txtConfirnPassword, $btnChange;
    $txtOldPassword = $('#txtOldPassword').dxTextBox({
        value: "",
        placeholder: "Enter old password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "before",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtOldPassword.option("mode", $txtOldPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Old password is required."
        }]
    }).dxTextBox('instance');

    $txtNewPassword = $('#txtNewPassword').dxTextBox({
        value: "",
        placeholder: "Enter new password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "before",
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
            type: "compare",
            comparisonTarget: function () {
                if ($txtOldPassword) {
                    return $txtOldPassword.option("value");
                }
            },
            message: "'Old Password' and 'New Password' should not be same.",
            comparisonType: "!="
        }, {
            type: "stringLength",
            min: 6,
            message: "New Password must have at least 6 symbols"
        }]
    }).dxTextBox('instance');

    $txtConfirnPassword = $('#txtConfirnPassword').dxTextBox({
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
                    $txtConfirnPassword.option("mode", $txtConfirnPassword.option("mode") === "text" ? "password" : "text");
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
        icon: "check",
        type: "success",
        text: "Change",
        height: 40,
        width: 180,
        useSubmitBehavior: true,
        template: function (data, container) {
            $("<div class='button-indicator'></div><span class='dx-button-text'>" + data.text + "</span>").appendTo(container);
            buttonIndicator = container.find(".button-indicator").dxLoadIndicator({
                visible: false
            }).dxLoadIndicator("instance");

        }
    }).dxButton('instance');

    $('#frmchangepassword').on('submit', function (e) {
        var $param = {};
        $param.username = app.common.userData.userName;
        $param.oldpassword = $txtOldPassword.option('value');
        $param.newpassword = $txtConfirnPassword.option('value');
        api.post(app.api.urls.signin.changePassword, $param).then(function (data) {
            if (data.message === app.api.statusMsg.Updated) {
                app.methods.msg.setMsgQueue(data.message);
                app.methods.url.redirectTo(app.page.urls.common.dashboard.getDashboard);
            }
            else if (data.message === app.api.statusMsg.InvalidUser) {
                api.successMsgHandler(data);
            }
        }).catch(api.errorHandler);
        e.preventDefault();
    })
});$(document).ready(function () {
    var $txtOldPassword, $txtNewPassword, $txtConfirnPassword, $btnChange;
    $txtOldPassword = $('#txtOldPassword').dxTextBox({
        value: "",
        placeholder: "Enter old password",
        showClearButton: true,
        mode: 'password',
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "la la-eye",
                type: "default",
                onClick: function () {
                    $txtOldPassword.option("mode", $txtOldPassword.option("mode") === "text" ? "password" : "text");
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Old password is required."
        }]
    }).dxTextBox('instance');

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
            type: "compare",
            comparisonTarget: function () {
                if ($txtOldPassword) {
                    return $txtOldPassword.option("value");
                }
            },
            message: "'Old Password' and 'New Password' should not be same.",
            comparisonType: "!="
        }, {
            type: "stringLength",
            min: 6,
            message: "New Password must have at least 6 symbols"
        }]
    }).dxTextBox('instance');

    $txtConfirnPassword = $('#txtConfirnPassword').dxTextBox({
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
                    $txtConfirnPassword.option("mode", $txtConfirnPassword.option("mode") === "text" ? "password" : "text");
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
        icon: "check",
        type: "success",
        text: "Change",
        height: 40,
        width: 180,
        useSubmitBehavior: true,
        template: function (data, container) {
            $("<div class='button-indicator'></div><span class='dx-button-text'>" + data.text + "</span>").appendTo(container);
            buttonIndicator = container.find(".button-indicator").dxLoadIndicator({
                visible: false
            }).dxLoadIndicator("instance");

        }
    }).dxButton('instance');

    $('#frmchangepassword').on('submit', function (e) {
        var $param = {};
        $param.username = app.common.userData.userName;
        $param.oldpassword = $txtOldPassword.option('value');
        $param.newpassword = $txtConfirnPassword.option('value');
        api.post(app.api.urls.signin.changePassword, $param).then(function (data) {
            if (data.message === app.api.statusMsg.PasswordChange) {
                app.methods.msg.setMsgQueue(data.message);
                app.methods.url.redirectTo(app.page.urls.common.dashboard.getDashboard);
            }
            else if (data.message === app.api.statusMsg.InvalidUser) {
                api.successMsgHandler(data);
            }
        }).catch(api.errorHandler);
        e.preventDefault();
    })
});