/// <reference path="../../../common/apiurls.js" />
/// <reference path="../../../common/constants.js" />
/// <reference path="../../../common/global.js" />

$(document).ready(function () {

    //grecaptcha.ready(function () {
    //    grecaptcha.execute('6LcShtkUAAAAAApVHQ5BpvkJZFBP0lWtTn2POLhI', { action: 'homepage' }).then(function (data) {
    //        console.log(data);
    //    })
    //});
    var $query = app.methods.url.urlSearchParams();
    var $txtName, $txtMobile, $txtEmail, $txtComments, $btnSubmitQuery;
    $txtName = $('#txtName').dxTextBox({
        value: "",
        placeholder: "Enter your name",
        showSpinButton: true,
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Your name is required."
        }]
    }).dxTextBox('instance');

    $txtMobile = $('#txtMobile').dxNumberBox({
        value: "",
        min:6000000000,
        placeholder: "Enter your mobile",
        showSpinButton: true,
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Your mobile is required."
        }]
    }).dxNumberBox('instance');

    $txtEmail = $('#txtEmail').dxTextBox({
        value: "",
        placeholder: "Enter your Email",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Email is required."
        }, {
                type: "email",
                message: "Email is invalid."
            }]
    }).dxTextBox('instance');

    $txtComments = $('#txtComments').dxTextArea({
        value: "", height: 90,
        placeholder: "Write your Query/Comments",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Query/Comments is required."
        }]
    }).dxTextArea('instance');

    $btnSubmitQuery = $("#btnSubmitQuery").dxButton({
        icon: "check",
        type: "success",
        text: "Send Message",
        useSubmitBehavior: true
    }).dxButton('instance');

    $('#frmSendQuery').on('submit', function (e) {
        var $captchaParam = { secret:'6LcShtkUAAAAABML2LPPb788393O0XHnwwAbd0Kh'};
        api.post('https://www.google.com/recaptcha/api/siteverify', $captchaParam).then(function (data) {
            console.log(data);
        }).catch(api.errorHandler);
        var $param = {};
        $param.Email = $txtEmail.option('value');
        $param.Name = $txtName.option('value');
        $param.Mobile = $txtMobile.option('value');
        $param.Query = $txtComments.option('value');
        $param.AppName = app.common.appName;
        api.post(app.api.urls.web.contactUs.sendQuery, $param).then(function (data) {
            api.successMsgHandler(data);
        }).catch(api.errorHandler);
        e.preventDefault();
    });


    api.get(app.api.urls.web.contactUs.getContactDetails).then(function (data) {
        if (data.data) {
            var resp = data.data;
            $('.contactTitle').text(resp.contactTitle);
            $('#contactDescription').html(resp.contactDescription);
            $('.address').text(resp.address);
            $('.mobile').text(resp.mobile);
            $('.email').text(resp.email).parent().attr('href', 'mailto:' + resp.email);
            //$ddlConsultationMethods.option('value', data.data.consultationMethods);

            if (resp.youtubeLink !== null && resp.youtubeLink.length > 0) {
                $('.youtubeLink').show().parent().attr('href', resp.youtubeLink);
            } else {
                $('.youtubeLink').hide();
            }
            if (resp.twitterLink !== null && resp.twitterLink.length > 0) {
                $('.twitterLink').show().parent().attr('href', resp.twitterLink);
            } else {
                $('.twitterLink').hide();
            }
            if (resp.instagramLink !== null && resp.instagramLink.length > 0) {
                $('.instagramLink').show().parent().attr('href', resp.instagramLink);
            } else {
                $('.instagramLink').hide();
            }
            if (resp.facebookLink !== null && resp.facebookLink.length > 0) {
                $('.facebookLink').show().parent().attr('href', resp.facebookLink);
            } else {
                $('.facebookLink').hide();
            }
            if (resp.googleLink !== null && resp.googleLink.length > 0) {
                $('.googleLink').show().parent().attr('href', resp.googleLink);
            } else {
                $('.googleLink').hide();
            }
        }
        else {
            app.toast.warning(app.userMsg.noRecord);
        }
    });
});