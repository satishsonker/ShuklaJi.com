$(document).ready(function () {
    var $query = app.methods.url.urlSearchParams();
    let $msg = $query.get('msg');
    let $code = $query.get('code');
    $('.msg-container-code').text($code);
    $('.msg-container-msg').text($msg);
    $('#btnlogin').dxButton({
        icon: "check",
        type: "success",
        text: "Login",
        onClick: function () {
            app.methods.url.redirectTo(app.page.urls.common.siginin.login);
        }
    }).dxButton('instance');
});