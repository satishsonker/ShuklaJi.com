var $txtRoleName;
var $txtRoleDisplayName;
$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    var $query = app.methods.url.urlSearchParams();
    $txtRoleName = $('#txtRoleName').dxTextBox({
        value: "",
        placeholder: "Enter role name.",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Role name is required."
        }, {
            type: "async",
            message: "Rolename is already exists.",
            validationCallback: function (params) {
                if (!$query.has('roleid')) {
                    return app.methods.api.isDataExists(app.api.proc.isDataExist.roleName, params.value);
                }
                else {
                    var d = $.Deferred();
                    d.resolve(true);
                    return d;
                }
            }
        }]
    }).dxTextBox('instance');

    $txtRoleDisplayName = $('#txtRoleDisplayName').dxTextBox({
        value: "",
        placeholder: "Enter role display name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Role display name is required."
        }, {
            type: "stringLength",
            min: 3,
            message: "Role display name must have at least 3 symbols."
        }]
    }).dxTextBox('instance');


    var $btn = $("#btnSave").dxButton({
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


    if ($query.has('roleid')) {
        api.post(app.api.urls.roleManagement.getRoles + `?roleId=${$query.get('roleid')}`).then(function (data) {
            if (data.data.length > 0) {
                $txtRoleDisplayName.option('value', data.data[0].displayName);
                $txtRoleName.option('value', data.data[0].roleName);
                $btn.option('text', 'Update');
            }
        });
    }



    $('#frmAddRole').on('submit', function (e) {

        var param = {};
        param.RoleName = $txtRoleName.option('value');
        param.RoleDisplayName = $txtRoleDisplayName.option('value');
        param.UserId = app.common.userId;
        param.RoleId = isNaN(parseInt($query.get('roleid'))) ? 0 : parseInt($query.get('roleid'));
        var $url = $btn.option('text') === 'Update' ? app.api.urls.roleManagement.updateRoles : app.api.urls.roleManagement.addRole;
        api.post($url, param).then(function (data) {
            api.successMsgHandler(data);
            if (param.RoleId === 0) {
                location.reload(true);
            }
            else {
                app.methods.url.redirectTo(app.page.urls.adminArea.roleManagement.roleList);
            }
        }).catch(api.errorHandler);

        e.preventDefault();
    });
});