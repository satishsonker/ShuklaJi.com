/// <reference path="../../lib/jquery/dist/jquery.min.js" />
/// <reference path="../common/apiurls.js" />
/// <reference path="../common/constants.js" />
/// <reference path="../common/global.js" />

$(document).ready(function () {
    app.methods.msg.readMsgQueue();
    api.get(app.api.urls.appSetting.getAppSetting).then(function (data) {
        if (data.data.length > 0) {
            $('.appLogo').attr('src', data.data[0].appLogo);
            $('.appName').text(data.data[0].appName);
            app.common.appName = data.data[0].appName;
            app.methods.storage.setData(app.methods.storage.storageKey.appSetting, data.data);
        }
        else {
            app.toast.warning('Unable to get App setting data.');
        }
    }).catch(api.errorHandler);
});