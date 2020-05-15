$(document).ready(function () {
    var $listWidget, $txtemailAdd, $groupEmails;

    api.get(app.api.urls.common.dropdownList + `?key=${app.api.proc.dropdownList.emailGroup}`).then(function (data) {
        //if (data.data.length > 0) {
        $ddlEmailGroup = $('#ddlEmailGroup').dxSelectBox({
            showClearButton: true,
            dataSource: new DevExpress.data.ArrayStore({
                data: data.data,
                key: "value",
                value: data.data[0].value
            }),
            displayExpr: "value",
            valueExpr: "value",
            onValueChanged: function (data) {
                api.get(app.api.urls.emailManagement.getEmailsByGroup + `?groupName=${data.value}`).then(function (data) {
                    $listWidget = $("#emailList").dxList({
                    dataSource: data.data,
                    height: 200,
                    allowItemDeleting: true,
                    itemDeleteMode: "static",
                    onSelectionChanged: updateSelectedItems,
                    onItemDeleted: updateSelectedItems
                }).dxList("instance");
                });
            }
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Email group name is required."
            }]
        }).dxSelectBox('instance');
        //}
    }).catch(api.errorHandler);
    var updateSelectedItems = function () {
        $groupEmails = $listWidget.option('dataSource');
        api.post(`${app.api.urls.emailManagement.updateEmailGroup}?groupname=${$ddlEmailGroup.option('value')}&emails=${$groupEmails.join(',')}`).then(function (data) {
            api.successMsgHandler(data);
            $txtemailAdd.option('value', '');
        }).catch(api.errorHandler);
    }

    $txtemailAdd = $('#txtemailAdd').dxTextBox({
        value: "",
        placeholder: "Enter Email to add in list",
        showClearButton: true,
        buttons: [{
            name: "password",
            location: "before",
            options: {
                icon: "la la-plus",
                type: "default",
                onClick: function () {
                    $groupEmails = $listWidget.option('dataSource');
                    var $newEmail = $txtemailAdd.option('value').trim();
                    if ($newEmail === '') {
                        app.toast.warning(`Please enter the email.`);
                        return;
                    }
                    if ($groupEmails.indexOf($newEmail) === -1) {
                        $groupEmails.push($newEmail);
                        $listWidget.option('dataSource', $groupEmails);
                        $listWidget.repaint();

                        api.post(`${app.api.urls.emailManagement.updateEmailGroup}?groupname=${$ddlEmailGroup.option('value')}&emails=${$groupEmails.join(',')}`).then(function (data) {
                            api.successMsgHandler(data);
                            $txtemailAdd.option('value', '');
                        }).catch(api.errorHandler);
                    }
                    else {
                        app.toast.warning(`${$newEmail} already exists.`);
                    }
                }
            }
        }]
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Email is required."
        }, {
                type: "email",
                message: "Email is invalid."
            }]
    }).dxTextBox('instance');
});