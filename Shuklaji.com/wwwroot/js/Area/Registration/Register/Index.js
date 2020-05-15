/// <reference path="../../../common/apiurls.js" />
/// <reference path="../../../common/constants.js" />
/// <reference path="../../../common/global.js" />
/// <reference path="../../../../lib/jquery/dist/jquery.js" />

$(function () {
    $(".dx-textbox").dxTextBox({
        value: "John Smith",
        placeholder: "Enter full name here...",
        showClearButton: true
    });

    //$("#placeholder").dxTextBox({
    //    placeholder: "Enter full name here..."
    //});

    //$("#clear-button").dxTextBox({
    //    value: "John Smith",
    //    showClearButton: true
    //});

    //$("#password").dxTextBox({
    //    mode: "password",
    //    placeholder: "Enter password",
    //    showClearButton: true,
    //    value: "f5lzKs0T",
    //});

    //$("#mask").dxTextBox({
    //    mask: "+1 (X00) 000-0000",
    //    maskRules: { "X": /[02-9]/ }
    //});

    //$("#disabled").dxTextBox({
    //    value: "John Smith",
    //    disabled: true
    //});

    //$("#full-name").dxTextBox({
    //    value: "Smith",
    //    showClearButton: true,
    //    placeholder: "Enter full name",
    //    valueChangeEvent: "keyup",
    //    onValueChanged: function (data) {
    //        emailEditor.option("value", data.value.replace(/\s/g, "").toLowerCase() + "@corp.com");
    //    }
    //});

    //var emailEditor = $("#email").dxTextBox({
    //    value: "smith@corp.com",
    //    readOnly: true,
    //    hoverStateEnabled: false
    //}).data("dxTextBox");
});