/// <reference path="../../lib/jquery/dist/jquery.min.js" />
/// <reference path="constants.js" />
/// <reference path="apiurls.js" />

let api = {};

api.get = function (url, options) {
    return new Promise(function (resolve, reject) {
        let option = {};

        option.url = url.indexOf('http') === -1 && url.indexOf('www') === -1 ? `${app.baseApiUrl}` + url : url; `${app.baseApiUrl}` + url;
        option.type = app.api.methodType.get;
        option.timeout = app.api.timeout;
        option.dataType = app.api.dataType;
        option.contentType = app.api.contentType.appJson;
        option.beforeSend = function (xhr) { xhr.setRequestHeader('authorization', app.common.token); },
            option.success = function (data, responseText, jqXHR) {
            resolve(data);
            var $responseData = jqXHR.getResponseHeader("refreshToken");
            if ($responseData!==null && $responseData.indexOf("Token") > -1) {
                $responseData = JSON.parse(jqXHR.getResponseHeader("refreshToken"));
                app.common.token = $responseData.Token;
            }
        };
        option.error = function (data) {
            reject(data);
        };

        $.extend(option, options);
        $.ajax(option);
    });
};

api.post = function (url, params, options) {
    return new Promise(function (resolve, reject) {
        let option = {};
        option.url = url.indexOf('http') === -1 && url.indexOf('www') === -1 ? `${app.baseApiUrl}` + url : url;
        option.method = app.api.methodType.post;
        option.timeout = app.api.timeout;
        option.dataType = app.api.dataType;
        option.contentType = app.api.contentType.appJson;
        option.data = JSON.stringify(params);
        option.beforeSend = function (xhr) { xhr.setRequestHeader('authorization', app.common.token); },
            option.success = function (data, responseText, jqXHR) {
            resolve(data);
            var $responseData = jqXHR.getResponseHeader("refreshToken");
            if ($responseData !==null && $responseData.indexOf("Token") > -1) {
                $responseData = JSON.parse(jqXHR.getResponseHeader("refreshToken"));
                app.common.token = $responseData.Token;
            }
        };
        option.error = function (data) {
            reject(data);
        };

        $.extend(option, options);
        $.ajax(option);
    });
};

api.postWithFile = function (url, params, options) {
    return new Promise(function (resolve, reject) {
        let option = {};
        option.url = url.indexOf('http') === -1 && url.indexOf('www') === -1 ? `${app.baseApiUrl}` + url : url;`${app.baseApiUrl}` + url;
        option.type = app.api.methodType.post;
        option.processData = false;
        option.contentType = false;
        option.data = params;
        option.beforeSend = function (xhr) { xhr.setRequestHeader('authorization', app.common.token); },
            option.success = function (data, res, jqXHR) {
            resolve(data);
            var $responseData = jqXHR.getResponseHeader("refreshToken");
            if ($responseData !==null && $responseData.indexOf("Token") > -1) {
                $responseData = JSON.parse(jqXHR.getResponseHeader("refreshToken"));
                app.common.token = $responseData.Token;
            }
        };
        option.error = function (data) {
            reject(data);
        };

        $.extend(option, options);
        $.ajax(option);
    });
};

// Toast Msg Start
app.toast.warning = function (msg, title, elapseTime) {
    title = title === undefined ? app.toast.title.warning : title;
    //toastCreator(app.toast.type.warning, msg, title, elapseTime);
    DevExpress.ui.notify({
        message: msg,
        position: {
            my: "center bottom",
            at: "right bottom"
        }
    }, app.toast.title.warning, app.toast.delayTime);
};

app.toast.success = function (msg, title, elapseTime) {
    title = title === undefined ? app.toast.title.success : title;
    DevExpress.ui.notify({
        message: msg,
        position: {
            my: "center bottom",
            at: "right bottom"
        }
    }, app.toast.title.success, app.toast.delayTime);
    //toastCreator(app.toast.type.success, msg, title, elapseTime);
};

app.toast.error = function (msg, title, elapseTime) {
    title = title === undefined ? app.toast.title.error : title;
    //toastCreator(app.toast.type.error, msg, title, elapseTime);
    DevExpress.ui.notify({
        message: msg,
        position: {
            my: "center bottom",
            at: "right bottom"
        }
    }, app.toast.title.error, app.toast.delayTime);
};

app.toast.info = function (msg, title, elapseTime) {
    title = title === undefined ? app.toast.title.ingo : title;
    //toastCreator(app.toast.type.info, msg, title, elapseTime);
    DevExpress.ui.notify({
        message: msg,
        position: {
            my: "center bottom",
            at: "right bottom"
        }
    }, app.toast.title.info, app.toast.delayTime);
};

let toastCreator = function (type, msg, title, elapseTime) {
    $('body').append(`
<div class="toast1" style="position: relative; min-height: 500px;">
    <!-- Position toasts -->
    <div style="position: absolute; bottom: 0; right: 0;min-width: ${app.toast.width}%;">
<div class="toast toast1 fade" id="toast-warning" data-delay="${app.toast.delayTime}">
    <div class="toast-header" style="background:${type}; !important">
        <strong class="mr-auto"><i class="la la-grav"></i> ${title}</strong>
        ${elapseTime !== undefined ? '<small>elapseTime</small>' : ''}
        <button type="button" class="ml-2 mb-1 close" data-dismiss="toast1">&times;</button>
    </div>
    <div class="toast-body">
        ${msg}
    </div>
</div>
</div>
</div>
`);
    $(".toast").toast('show');
};
// Toast Msg End

// API Error Handler
api.errorHandler = function (x, y, z) {
    if (x !== undefined && x.responseJSON !== undefined && x.responseJSON.errors !== undefined) {
        $('#summary-container').show();
        let $errorMsg = '';
        for (const key in x.responseJSON.errors) {
            $errorMsg += `<li>${x.responseJSON.errors[key].toString()}</li>`;
        }
        $('#summary').show().append(`<ul>${$errorMsg}</ul>`);
    }
    if (x !== undefined && x.status === 401) {
        var $responseMsg = JSON.parse(x.responseText);
        let $userMsg;
        if ($responseMsg.message === app.api.statusMsg.InvalidToken) {
            $userMsg = app.userMsg.invalidToken;
        }
        else if ($responseMsg.message === app.api.statusMsg.ExpireToken) {
            $userMsg = app.userMsg.expireToken;
        }
        app.methods.url.redirectTo(app.page.urls.common.errorHandler.unauthorize + `?msg=${$userMsg}&code=${x.status}`);
    }
    else if (x !== undefined && x.status === 500) {
        let $userMsg = app.userMsg.internalServerError;
        app.methods.url.redirectTo(app.page.urls.common.errorHandler.unauthorize + `?msg=${$userMsg}&code=${x.status}`);
    }
    else if (x !== undefined && x.status === 400) {
        app.toast.warning(app.userMsg.badRequest);
    }
    else if (x !== undefined && x.status === 0) {
       // let $userMsg= app.userMsg.connectionTimeOut;
        //app.methods.url.redirectTo(app.page.urls.common.errorHandler.unauthorize + `?msg=${$userMsg}&code=${x.status}`);
    }
};
//

//Ready Event
$(document).ajaxStart(function () {
    $('body').append(`<div class="loader"> <div class="spinner">
            <div class="spinner-loader"></div>
            <div class="spinner-loader-text">Loading...</div>
        </div></div>`);
});

$(document).ajaxComplete(function () {
    $('.loader').remove();
});


//Common Methods
app.methods = {};
app.methods.api = {
    isDataExists: function (key, value) {
        var d = $.Deferred();
        api.get(`${app.api.urls.common.isDataExists}?key=${key}&value=${value}`).then(function (data) {
            d.resolve(data.data);
        }).catch(function (error) {
            d.reject(error);
        });
        return d.promise();
    }
};

app.methods.url = {
    urlSearchParams: function () {
        return new URLSearchParams(window.location.search);
    },
    redirectTo: function (url) {
        location.href = url;
    },
    reloadPage: function () {
        location.reload(true);
    }
};

app.methods.msg = {
    setMsgQueue: function (msg) {
        var $data = app.methods.storage.getData(app.methods.storage.storageKey.msgQueue);
        try {
            $data = JSON.parse($data);
            $data = $data === null ? [] : $data;
            $data.push(msg);
            app.methods.storage.setData(app.methods.storage.storageKey.msgQueue, JSON.stringify($data));
        } catch (e) {
            $data = [];
        }
    },
    readMsgQueue: function () {
        var $data = app.methods.storage.getData(app.methods.storage.storageKey.msgQueue);
        try {
            $data = JSON.parse($data);
            $data = $data === null ? [] : $data;
            $($data).each(function (i, e) {
                api.successMsgHandler({ message: e });
            });
            $data = [];
            app.methods.storage.setData(app.methods.storage.storageKey.msgQueue, JSON.stringify($data));
        } catch (e) {
            $data = [];
        }
    }
};

app.methods.storage = {
    getData: function (key) {
        return localStorage.getItem(key);
    },
    setData: function (key, data) {
        if (typeof data === 'string') {
            localStorage.setItem(key, data);
        }
        else if (typeof data === 'object') {
            localStorage.setItem(key, JSON.stringify(data));
        }
    },
    storageKey: {
        msgQueue: 'msgQueue',
        loginData: 'loginData',
        appSetting:'appSetting'
    }
};

app.methods.page = {
    deleteConfirm: function (confirmHandler,param,msg) {
        $('#delete-container').show();
        msg = msg === undefined || msg === null ? 'You want to delete the record. Are you sure?' : msg;
        $('#lblDeletePopupMsg').text(msg);
        $('#btnDeletePopupYes').dxButton({
            icon: "trash",
            type: "success",
            text: "Yes",
            useSubmitBehavior: true,
            onClick: function () {
                confirmHandler(param);
                $('#icloseDelete').click();
            }
        });
        $('#btnDeletePopupNo').dxButton({
            icon: "close",
            type: "danger",
            text: "No",
            useSubmitBehavior: true,
            onClick: function () {
                $('#icloseDelete').click();
            }
        });

        $('#icloseDelete').on('click', function () {
            $('#' + $(this).data('close')).hide();
        });
    },
    signOut: function () {
        app.methods.storage.setData(app.methods.storage.storageKey.loginData, {});
        app.methods.url.redirectTo(app.page.urls.common.siginin.login);
    }
};

app.methods.common = {
    getGridDateTime: function ($dateString) {
        return `${new Date($dateString).toLocaleDateString()} - ${new Date($dateString).toLocaleTimeString()}`;
    },
    setDataAttr: function ($data) {
        return JSON.stringify($data).replace(/\"/g, "'");
    },
    getDataAttr: function ($data) {
        return JSON.parse($data.replace(/\'/g, '"'));
    },
    encodeHtml:function ($html) {
        return $html.replace(/&/g, '&amp;')
            .replace(/>/g, '&gt;')
            .replace(/</g, '&lt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&apos;');
    },
    getSqlDate: function ($date) {
        if (typeof $date === 'string') {
            $date = new Date($date);
        }
        return `${$date.getMonth() + 1}/${$date.getDate()}/${$date.getFullYear()}`;
    }
}
//End Common Methods

app.common = {
    userId: 1,
    token: '',
    userData: {},
    appName: '',
    userName: '',
    userRole:''
};

api.successMsgHandler = function (data) {
    if (data.message === app.api.statusMsg.Saved) {
        app.toast.success(app.userMsg.recordSaved);
    }
    else if (data.message === app.api.statusMsg.Exist) {
        app.toast.warning(app.userMsg.alreadyExist);
    }
    else if (data.message === app.api.statusMsg.Error) {
        app.toast.error(app.userMsg.errAtServer);
    }
    else if (data.message === app.api.statusMsg.Updated) {
        app.toast.success(app.userMsg.recordUpdated);
    }
    else if (data.message === app.api.statusMsg.Deleted) {
        app.toast.success(app.userMsg.recordDeleted);
    }
    else if (data.message === app.api.statusMsg.NoRecord) {
        app.toast.warning(app.userMsg.noRecord);
    }
    else if (data.message === app.api.statusMsg.NoDataFromClient) {
        app.toast.error(app.userMsg.noDataFromClient);
    }
    else if (data.message === app.api.statusMsg.InvalidUser) {
        app.toast.warning(app.userMsg.InvalidUser);
    }
    else if (data.message === app.api.statusMsg.BlockUser) {
        app.toast.warning(app.userMsg.blockUser);
    }
    else if (data.message === app.api.statusMsg.ValidUser) {
        app.toast.success(app.userMsg.validUser);
    }
    else if (data.message === app.api.statusMsg.InUse) {
        app.toast.warning(app.userMsg.inUse);
    }
    else if (data.message === app.api.statusMsg.PasswordChange) {
        app.toast.success(app.userMsg.passwordChange);
    }
    else if (data.message === app.api.statusMsg.ResetCode) {
        app.toast.success(app.userMsg.resetCode);
    }
    else if (data.message === app.api.statusMsg.InvalidResetLink) {
        app.toast.error(app.userMsg.invalidResetLink);
    }
    else if (data.message === app.api.statusMsg.EmailSend) {
        app.toast.success(app.userMsg.emailSend);
    }
    else if (data.message === app.api.statusMsg.ReplySend) {
        app.toast.success(app.userMsg.replySend);
    }
    else if (data.message === app.api.statusMsg.BadRequest) {
        app.toast.success(app.userMsg.badRequest);
    }
    else {
        app.toast.success(app.userMsg.invalidResponse);
    }

    app.methods.msg.setMsgQueue(data.message);
};



