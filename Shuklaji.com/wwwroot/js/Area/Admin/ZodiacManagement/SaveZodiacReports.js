var $reportType = [{ id: 1, value: "Daily" }, { id: 2, value: "Weekly" }, { id:3, value: "Monthly" }, { id: 4, value: "Yearly" }]
var $currentDate = new Date();
var $fromDate = new Date();

$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    let $txtRangeFrom, $txtRangeTo, $txtDescription, $ddlZodiac, $ddlReportType, $btn;
    var $query = app.methods.url.urlSearchParams();
    $txtRangeFrom = $('#txtRangeFrom').dxDateBox({
        type: "datetime",
        value: $currentDate,
        placeholder: "Enter From Date",
        showClearButton: true,
        min: $currentDate,
        onValueChanged: function (data) {
            $txtRangeTo.option('min', new Date(data.value));
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "From date is required."
        }]
    }).dxDateBox('instance');

    $txtRangeTo = $('#txtRangeTo').dxDateBox({
        type: "datetime",
        value: $fromDate,
        placeholder: "Enter To Date",
        showClearButton: true,
        min: new Date($txtRangeFrom.option('value')),
        onValueChanged: function (data) {
            $txtRangeFrom.option('max', new Date(data.value));
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "FrToom date is required."
        }]
    }).dxDateBox('instance');


    api.get(app.api.urls.common.dropdownList + `?key=${app.api.proc.dropdownList.zodiac}`).then(function (data) {
        if (data.data.length > 0) {
            $ddlZodiac = $('#ddlZodiac').dxSelectBox({
                placeholder: "Choose Zodiac",
                showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                    data: data.data,
                    key: "id"
                }),
                displayExpr: "value",
                valueExpr: "id"
            }).dxValidator({
                validationRules: [{
                    type: "required",
                    message: "Zodiac is required."
                }]
            }).dxSelectBox('instance');

            $ddlReportType = $('#ddlReportType').dxSelectBox({
                placeholder: "Choose Report Type",
                showClearButton: true, dataSource: new DevExpress.data.ArrayStore({
                    data: $reportType,
                    key: "value"
                }),
                displayExpr: "value",
                valueExpr: "value",
                onValueChanged: function (data) {
                    var $type = data.value.toLowerCase();
                    $fromDate = new Date();
                    if ($type === "daily")
                        $txtRangeTo.option('value',new Date($fromDate.setDate($currentDate.getDate() + 1)));
                    else if ($type === "weekly")
                        $txtRangeTo.option('value', new Date($fromDate.setDate($currentDate.getDate() + 7)));
                    else if ($type === "monthly")
                        $txtRangeTo.option('value', new Date($fromDate.setDate($currentDate.getDate() + 30)));
                    else if ($type === "yearly")
                        $txtRangeTo.option('value', new Date($fromDate.setDate($currentDate.getDate() + 365)));
                    else
                        $txtRangeTo.option('value',new Date());
                }
            }).dxValidator({
                validationRules: [{
                    type: "required",
                    message: "Report type is required."
                }]
            }).dxSelectBox('instance');
            if ($query.has('reportid')) {
                api.get(app.api.urls.zodiacManagement.getZodiacReport + `?zodiacreportid=${$query.get('reportid')}`).then(function (data) {
                    if (data.data.length > 0) {
                        $ddlReportType.option('value', data.data[0].reportType);
                        $ddlZodiac.option('value', data.data[0].zodiacId);
                        $txtRangeFrom.option('value', data.data[0].rangeFrom);
                        $txtRangeTo.option('value', data.data[0].rangeTo);
                        $txtDescription.option('value', data.data[0].description);
                        $btn.option('text', 'Update');
                    }
                }).catch(api.errorHandler);
            }
            else {
                $btn.option('text', 'Save');
            }
        }
    }).catch(api.errorHandler);

    $txtDescription = $('.html-editor').dxHtmlEditor({
        height: 200,
        toolbar: {
            items: [
                "undo", "redo", "separator",
                {
                    formatName: "size",
                    formatValues: ["8pt", "10pt", "12pt", "14pt", "18pt", "24pt", "36pt"]
                },
                {
                    formatName: "font",
                    formatValues: ["Arial", "Courier New", "Georgia", "Impact", "Lucida Console", "Tahoma", "Times New Roman", "Verdana"]
                },
                "separator", "bold", "italic", "strike", "underline", "separator",
                "alignLeft", "alignCenter", "alignRight", "alignJustify", "separator",
                {
                    formatName: "header",
                    formatValues: [false, 1, 2, 3, 4, 5]
                }, "separator",
                "orderedList", "bulletList", "separator",
                "color", "background", "separator",
                "link", "image", "separator",
                "clear", "codeBlock", "blockquote"
            ]
        },
        mediaResizing: {
            enabled: true
        }
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Email Template is required."
        }]
    }).dxHtmlEditor('instance');

    $btn = $("#btnSave").dxButton({
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

    

    $('#frmAddZodiacReport').on('submit', function (e) {

        var param = {};
        param.ZodiacId = $ddlZodiac.option('value');
        param.ReportType = $ddlReportType.option('value');
        param.Description = $txtDescription.option('value');
        param.RangeFrom = $txtRangeFrom.option('value');
        param.RangeTo = $txtRangeTo.option('value');
        param.UserId = app.common.userId;
        param.ZodiacReportId = isNaN(parseInt($query.get('reportid'))) ? 0 : parseInt($query.get('reportid'));
        var $url = $btn.option('text') === 'Update' ? app.api.urls.zodiacManagement.updateZodiacReport : app.api.urls.zodiacManagement.saveZodiacReport;
        api.post($url, param).then(function (data) {
            api.successMsgHandler(data);
            if (param.RoleId === 0) {
                location.reload(true);
            }
            else {
                app.methods.url.redirectTo(app.page.urls.adminArea.zodiacManagement.zodiacReport);
            }
        }).catch(api.errorHandler);

        e.preventDefault();
    });
});