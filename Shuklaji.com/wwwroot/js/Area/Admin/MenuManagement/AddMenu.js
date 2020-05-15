$(document).ready(function () {
    var $ddlParent, $txtManuName, $parentMenuData,$txtBadge, $txtMenuDisplayName, $txtIconClass, $txtUrl, $txtIconColor, $numDisplayOrder, $ddlPosition, $ddlAccessBy;
    var $accessbyData = [{ id: 1, value: 'Admin' }, { id: 2, value: 'User' }];
    $("#summary").dxValidationSummary({});
    var $query = app.methods.url.urlSearchParams();
    let $menuId = parseInt($query.get('menuid'));
    $menuId = isNaN($menuId) ? 0 : $menuId;
    api.get(app.api.urls.menuManagement.getMenu + `?menuid=${$menuId}`).then(function (data) {
        $parentMenuData = data.data;
        $ddlParent = $('#ddlParent').dxSelectBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.data.filter(x => x.position === $menuPosition[0]),
                key: "menuId"
            }),
            displayExpr: "displayName",
            valueExpr: "menuId",
            placeholder: "Choose Parent Menu",
            showClearButton: true,
            searchEnabled: true,
            searchExpr: ['displayName']
        }).dxSelectBox('instance');

        $ddlAccessBy = $('#ddlAccessBy').dxSelectBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: $accessbyData,
                key: "value"
            }),
            displayExpr: "value",
            valueExpr: "value",
            placeholder: "Choose Access by",
            showClearButton: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Access by is required."
            }]
        }).dxSelectBox('instance');

        if ($menuId > 0) {
            $ddlParent.option('value', $parentMenuData[0].parentMenuId);
            $ddlAccessBy.option('value', $parentMenuData[0].accessBy);
            $txtManuName.option('value', $parentMenuData[0].menuName);
            $txtMenuDisplayName.option('value', $parentMenuData[0].displayName);
            $txtIconClass.option('value', $parentMenuData[0].iconClass);
            $txtUrl.option('value', $parentMenuData[0].menuPath);
            $txtBadge.option('value', $parentMenuData[0].badge);
            $txtIconColor.option('value', $parentMenuData[0].iconColor);
            $numDisplayOrder.option('value', $parentMenuData[0].displayOrder);
            $ddlPosition.option('value', $parentMenuData[0].position);
            $btn.option('text', 'Update');
        }
    });
    let $menuPosition = ['Left', 'Top'];
    $txtManuName = $('#txtMenuName').dxTextBox({
        value: "",
        placeholder: "Enter menu name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Menu name is required."
        },
        {
            type: "pattern",
            pattern: /^[^\s]+$/,
            message: "Whitespace is not allowed in menu name."
        }]
    }).dxTextBox('instance');

    $txtMenuDisplayName = $('#txtMenuDisplayName').dxTextBox({
        value: "",
        placeholder: "Enter menu display name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Menu display name is required."
        }]
    }).dxTextBox('instance');

    $txtBadge = $('#txtBadge').dxTextBox({
        value: "",
        placeholder: "Enter menu badge name",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "stringLength",
            max: 10,
            message: "Maximum 10 symbols are allowed"
        }]
    }).dxTextBox('instance');

    $txtUrl = $('#txtUrl').dxTextBox({
        value: "",
        placeholder: "Enter URL",
        showClearButton: true
    }).dxTextBox('instance');

    $txtIconClass = $('#txtIconClass').dxTextBox({
        value: "",
        placeholder: "Enter Icon Class",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Icon class is required."
        }]
    }).dxTextBox('instance');

    $txtIconColor = $('#txtIconColor').dxTextBox({
        value: "",
        placeholder: "Enter Icon Color",
        showClearButton: true
    }).dxTextBox('instance');

    $numDisplayOrder = $('#numDisplayOrder').dxNumberBox({
        value: "",
        placeholder: "Enter display order",
        format: '##',
        min: 0,
        max: 100,
        showSpinButton: true,
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Display order is required."
        }]
    }).dxNumberBox('instance');

    $ddlPosition = $('#ddlPosition').dxSelectBox({
        items: $menuPosition,
        placeholder: "Choose Position",
        showClearButton: true,
        value: $menuPosition[0],
        onValueChanged: function (data) {
            $ddlParent.option('dataSource', new DevExpress.data.ArrayStore({
                data: $parentMenuData.filter(x => x.position === data.value),
                key: "menuId"
            }));
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Display order is required."
        }]
    }).dxSelectBox('instance');

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

    $('#frmAddMenu').on('submit', function (e) {
        saveUpdateMenu();
        e.preventDefault();
        $ddlParent.option('value', '');
        $txtManuName.option('value', '');
        $txtUrl.option('value', '');
        $txtIconClass.option('value', '');
        $txtIconColor.option('value', '');
        $txtMenuDisplayName.option('value', '');
        $numDisplayOrder.option('value', '');
        $ddlPosition.option('value', '');
        $txtBadge.option('value', '');
    });

    var saveUpdateMenu = function () {
        var $param = {};
        $param.ParentMenuId = $ddlParent.option('value');
        $param.MenuName = $txtManuName.option('value');
        $param.Badge = $txtBadge.option('value');
        $param.MenuPath = $txtUrl.option('value');
        $param.IconClass = $txtIconClass.option('value');
        $param.IconColor = $txtIconColor.option('value');
        $param.DisplayName = $txtMenuDisplayName.option('value');
        $param.DisplayOrder = $numDisplayOrder.option('value');
        $param.Position = $ddlPosition.option('value');
        $param.AccessBy = $ddlAccessBy.option('value');
        $param.UserId = app.common.userId;
        $param.MenuId = $menuId;
        var $url = $menuId > 0 ? app.api.urls.menuManagement.updateMenu : app.api.urls.menuManagement.addMenu;
        api.post($url, $param).then(function (data) {
            api.successMsgHandler(data);
            $btn.option('text', 'Save');
            app.methods.url.redirectTo(app.page.urls.adminArea.menuManagement.menuList);
        }).catch(api.errorHandler);
    };
});