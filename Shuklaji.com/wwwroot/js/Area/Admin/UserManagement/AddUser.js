$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    let $txtUserName, $txtFirstName, $txtLastName, $ddlUserRole, $txtEmail, $txtMobile, $photo, $ddlGender, $txtDob;
    var $query = app.methods.url.urlSearchParams();
    $photo = $("#file-uploader").dxFileUploader({
        selectButtonText: "Select photo",
        labelText: "",
        accept: "image/*",
        uploadMode: "useForm", maxFileSize: 50000, allowedFileExtensions: [".jpg", ".jpeg",".png"],
        onValueChanged: function (e) {
            var files = e.value;
            if (files.length > 0) {
                $("#selected-files .selected-item").remove();
                $.each(files, function (i, file) {
                    var $selectedItem = $("<div />").addClass("selected-item");
                    $selectedItem.append(
                        $("<span />").html("Name: " + file.name + "<br/>"),
                        $("<span />").html("Size " + file.size + " bytes" + "<br/>"),
                        $("<span />").html("Max size 50 Kb<br/>")
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
    $txtUserName = $('#txtUserName').dxTextBox({
        value: "",
        placeholder: "Enter user name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "User name is required."
        }, {
            type: "async",
            message: "Username is already exists.",
            validationCallback: function (params) {
                if (!$query.has('userid')) {
                    return app.methods.api.isDataExists(app.api.proc.isDataExist.userName, params.value);
                }
                else {
                    var d = $.Deferred();
                    d.resolve(true);
                    return d;
                }
            }
        }]
    }).dxTextBox('instance');

    $txtFirstName = $('#txtFirstName').dxTextBox({
        value: "",
        placeholder: "Enter first name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "First name is required."
        }]
    }).dxTextBox('instance');

    $txtMobile = $('#txtMobile').dxTextBox({
        value: "",
        placeholder: "Enter mobile number",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Mobile number is required."
        }]
    }).dxTextBox('instance');

    $txtDob = $('#txtDob').dxDateBox({
        value: "",
        placeholder: "Enter date of birth",
        showClearButton: true,
        max: new Date(),
        min: new Date(new Date().setFullYear(-100))
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Date of birth is required."
        }]
    }).dxDateBox('instance');

    $txtEmail = $('#txtEmail').dxTextBox({
        value: "",
        placeholder: "Enter email address",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Email is required."
        }, {
            type: "email",
            message: "Invalid email."
        }]
    }).dxTextBox('instance');

    $txtLastName = $('#txtLastName').dxTextBox({
        value: "",
        placeholder: "Enter last name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Last name is required."
        }]
    }).dxTextBox('instance');

    api.get(app.api.urls.common.dropdownList + `?key=${app.api.proc.dropdownList.role}`).then(function (data) {
        if (data.data.length > 0) {
            $ddlUserRole = $('#ddlUserRole').dxSelectBox({
                placeholder: "Choose Role",
                showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                    data: data.data,
                    key: "id"
                }),
                displayExpr: "value",
                valueExpr: "id"
            }).dxValidator({
                validationRules: [{
                    type: "required",
                    message: "Role is required."
                }]
            }).dxSelectBox('instance');

            $ddlGender = $('#ddlGender').dxSelectBox({
                placeholder: "Choose Gender",
                showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                    data: [{ id: 1, value: "Male" }, { id: 2, value: "Female" }],
                    key: "id"
                }),
                displayExpr: "value",
                valueExpr: "value"
            }).dxValidator({
                validationRules: [{
                    type: "required",
                    message: "Gender is required."
                }]
            }).dxSelectBox('instance');

            if ($query.has('userid')) {
                api.get(app.api.urls.userManagement.getUser + `?userId=${$query.get('userid')}`).then(function (data) {
                    if (data.data.length > 0) {
                        $txtFirstName.option('value', data.data[0].firstName);
                        $txtLastName.option('value', data.data[0].lastName);
                        $ddlGender.option('value', data.data[0].gender);
                        $txtDob.option('value', data.data[0].dob);
                        $txtUserName.option('value', data.data[0].userName);
                        $ddlUserRole.option('value', data.data[0].roleId);
                        $txtMobile.option('value', data.data[0].mobile);
                        $txtEmail.option('value', data.data[0].email);
                        $("#imgLogo").attr("src", data.data[0].photo);
                        $btn.option('text', 'Update');
                    }
                });
            }
        }
    }).catch(api.errorHandler);

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



    $('#frmAddUser').on('submit', function (e) {

        var $formData = new FormData();
        $formData.append('file', $photo.option('value')[0]);
        $formData.append('UserName', $txtUserName.option('value'));
        $formData.append('FirstName', $txtFirstName.option('value'));
        $formData.append('Gender', $ddlGender.option('value'));
        $formData.append('Dob', app.methods.common.getSqlDate($txtDob.option('value')));
        $formData.append('Email', $txtEmail.option('value'));
        $formData.append('Mobile', $txtMobile.option('value'));
        $formData.append('LastName', $txtLastName.option('value'));
        $formData.append('RoleId', $ddlUserRole.option('value'));
        $formData.append('AppName', app.common.appName);
        $formData.append('UserId', isNaN(parseInt($query.get('userid'))) ? 0 : parseInt($query.get('userid')));
        $formData.append('CreatedBy', app.common.userId);
        var $url = $btn.option('text') === 'Update' ? app.api.urls.userManagement.updateUser : app.api.urls.userManagement.addUser;
        api.postWithFile($url, $formData).then(function (data) {
            api.successMsgHandler(data);
            if (param.RoleId === 0) {
                location.reload(true);
            }
            else {
                app.methods.url.redirectTo(app.page.urls.adminArea.userManagement.userList);
            }
        }).catch(api.errorHandler);
        e.preventDefault();
    });
});